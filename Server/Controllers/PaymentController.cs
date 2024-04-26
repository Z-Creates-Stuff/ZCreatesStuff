﻿using LouiseTieDyeStore.Server.Services.PaymentService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LouiseTieDyeStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("checkout")]
        public ActionResult<string> CreateCheckoutSession(CheckoutDTO checkout)
        {
            var session = _paymentService.CreateCheckoutSession(checkout);
            return Ok(session.Url);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<bool>>> FullfillOrder()
        {
            Console.WriteLine("!!! Request: " + JsonConvert.SerializeObject(Request));
            var response = await _paymentService.FullfillOrder(Request);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }
    }
}
