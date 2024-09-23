﻿namespace Basket.Api.Basket.GetBasket;

public record GetBasketQuery(string UserName): IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart ShoppingCart);

public class GetBasketQueryHandler(IBasketRepository basketRepository) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var basket = await basketRepository.GetBasket(request.UserName);

        return new GetBasketResult(basket);

    }
}

