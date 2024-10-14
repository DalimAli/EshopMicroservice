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
}
