﻿using LouiseTieDyeStore.Shared.FedExRequestResponse.AuthToken;
using LouiseTieDyeStore.Shared.FedExRequestResponse.RateQuote;
using LouiseTieDyeStore.Shared.FedExRequestResponse.ValidateAddress;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Security.Policy;

namespace LouiseTieDyeStore.Server.Services.ShippingService
{
    public class FedExShippingService : IShippingService
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;

        public FedExShippingService(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<string> GetAuthToken()
        {
            // TODO: Do I abstract away this client setup?

            string clientId = Environment.GetEnvironmentVariable("FedExKeys_ClientId") 
                ?? _config["FedExKeys:ClientId"];
            string clientSecret = Environment.GetEnvironmentVariable("FedExKeys_ClientSecret")
                ?? _config["FedExKeys:ClientSecret"];

            var dict = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", clientId },
                { "client_secret", clientSecret }
            };

            var client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri("https://apis.fedex.com");

            using var req = new HttpRequestMessage(HttpMethod.Post, "/oauth/token") { Content = new FormUrlEncodedContent(dict) };
            using var res = await client.SendAsync(req);

            if (res != null)
            {
                var tokenObject = JsonConvert.DeserializeObject<FedExAuthTokenResponse>(await res.Content.ReadAsStringAsync());

                var token = tokenObject.AccessToken;

                return token;
            }
            else
            {
                return "Unsuccessful Request";
            }
        }

        public async Task<ServiceResponse<string>> GetShippingRateQuote(ShippingInfoDTO shippingInfo, string? authToken = null)
        {
            
            authToken = await GetAuthToken(); // Comment out if Validating Address first

            var shippingInfoRequest = new FedExRateQuoteRequest
            {
                AccountNumber = new AccountNumber
                {
                    Value = Environment.GetEnvironmentVariable("FedExKeys_AccountNumber")
                    ?? _config["FedExKeys:AccountNumber"],
                },
                RequestedShipment = new RequestedShipment
                {
                    Shipper = new Recipient
                    {
                        Address = new Shared.FedExRequestResponse.RateQuote.Address
                        {
                            PostalCode = _config["Shipping:LocalPostalCode"]
                        }
                    },
                    Recipient = new Recipient
                    {
                        Address = new Shared.FedExRequestResponse.RateQuote.Address
                        {
                            PostalCode = shippingInfo.Zip
                        }
                    },
                    PickupType = "DROPOFF_AT_FEDEX_LOCATION",
                    RateRequestType = new List<string>
                    {
                        "ACCOUNT",
                        "PREFERRED"
                    },
                    RequestedPackageLineItems = new List<RequestedPackageLineItem>
                    {
                        new RequestedPackageLineItem
                        {
                            Weight = new Weight
                            {                               
                                Value = shippingInfo.ItemCount * int.Parse(_config["Shipping:PoundsPerItem"])
                            }
                        }
                    }
                },
                CarrierCodes = new List<string>
                {
                    "FDXG"
                }
            };

            var client = _httpClientFactory.CreateClient("fedExApi");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + authToken);

            try
            {
                var result = await client.PostAsJsonAsync("/rate/v1/rates/quotes", shippingInfoRequest);

                //Console.WriteLine("!!!Quote Result: " + await result.Content.ReadAsStringAsync());

                var quoteResponse = JsonConvert.DeserializeObject<FedExRateQuoteResponse>(await result.Content.ReadAsStringAsync());

                //Console.WriteLine("!!!Quot Object : " + JsonConvert.SerializeObject(quoteResponse));

                string shippingCost = quoteResponse.Output.RateReplyDetails[0].RatedShipmentDetails[0].TotalNetCharge.ToString();

                return new ServiceResponse<string>
                {
                    Data = shippingCost
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<string>> ValidateShippingAddress(ShippingInfoDTO shippingInfo)
        {
            //Console.WriteLine("!!! shipping info:  " + JsonConvert.SerializeObject(shippingInfo));

            var requestAddress = new FedExValidateAddressRequest
            {
                AddressesToValidate = new List<AddressToValidate>
                {
                    new AddressToValidate
                    {
                        Address = new Shared.FedExRequestResponse.ValidateAddress.Address
                        {
                            StreetLines = new List<string>
                            {
                                shippingInfo.LineOne,
                                shippingInfo.LineTwo
                            },
                            City = shippingInfo.City,
                            PostalCode = shippingInfo.Zip.ToString()
                        }
                    }
                }
            };

            //Console.WriteLine("!!!Request Address: " + JsonConvert.SerializeObject(requestAddress));

            var authToken = await GetAuthToken();

            var client = _httpClientFactory.CreateClient("fedExApi");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + authToken);

            try
            {
                var result = await client.PostAsJsonAsync("/address/v1/addresses/resolve", requestAddress);

                //Console.WriteLine("!!!Address Result: " + await result.Content.ReadAsStringAsync());

                var returnObject = JsonConvert.DeserializeObject<FedExValidateAddressResponse>(await result.Content.ReadAsStringAsync());

                var matched = returnObject.Output.ResolvedAddresses[0].Attributes.Matched; // Bool for Valid Address
                //Console.WriteLine("!!! Matched: " + JsonConvert.SerializeObject(matched));

                //Console.WriteLine("!!! Address Object: " + JsonConvert.SerializeObject(returnObject));

                return await GetShippingRateQuote(shippingInfo, authToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine("!!!Exception: " + ex.Message);

                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Invalid Address"
                };
            }          
        }
    }
}
