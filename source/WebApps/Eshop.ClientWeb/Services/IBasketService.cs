﻿using System.Net;

namespace Eshop.ClientWeb.Services;

public interface IBasketService
{
    [Get("/basket-service/basket/{userName}")]
    Task<GetBasketResponse> GetBasketAsync(string userName);

    [Post("/basket-service/basket")]
    Task<StoreBasketResponse> StoreBasketAsync(StoreBasketRequest request);

    [Delete("/basket-service/basket/{userName}")]
    Task<DeleteBasketResponse> DeleteBasketAsync(string userName);

    [Post("/basket-service/basket/checkout")]
    Task<CheckoutBasketResponse> CheckoutBasketAsync(CheckoutBasketRequest request);

    public async Task<ShoppingCartModel> LoadUserBasket()
    {
        // Get Basket If Not Exist Create New Basket with Default Logged In User Name: swn
        var userName = "sms";
        ShoppingCartModel basket;

        try
        {
            var getBasketResponse = await GetBasketAsync(userName);
            basket = getBasketResponse.ShoppingCart;
        }
        catch (ApiException apiException) when (apiException.StatusCode == HttpStatusCode.NotFound)
        {
            basket = new ShoppingCartModel
            {
                UserName = userName,
                Items = []
            };
        }

        return basket;
    }
}
