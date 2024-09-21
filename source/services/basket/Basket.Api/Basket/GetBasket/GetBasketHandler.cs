namespace Basket.Api.Basket.GetBasket;

public record GetBasketQuery(string UserName): IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart ShoppingCart);

public class GetBasketHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        // TODO get basker from database
        //var basket = new _repository.GetBasket(request.UserName);

        return new GetBasketResult(new ShoppingCart("sms"));

    }
}

