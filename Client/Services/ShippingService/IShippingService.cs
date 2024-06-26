﻿namespace LouiseTieDyeStore.Client.Services.ShippingService
{
    public interface IShippingService
    {
        List<string> LocalZipCodes { get; }
        Task<ServiceResponse<ShippingResponse>> GetShippingCost(ShippingInfoDTO shippingInfo);
    }
}
