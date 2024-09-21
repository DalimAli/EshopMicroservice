namespace Basket.Api.Basket.GetBasket;

public record GetBasketRequest(string UserName);

public record GetBasketResponse(ShoppingCart ShoppingCart);

public class GetBasketEndPoint : ICarterModule 
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{UserName}", async (string UserName, ISender sender) =>
        {
            var result = await sender.Send(new GetBasketQuery(UserName));
            var response = result.Adapt<GetBasketResponse>();

            return Results.Ok(response);
        })
            .WithName("gets")
            .Produces<GetBasketResponse>(statusCode: StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Basket")
            .WithDescription("Get user Basket");
    }
}

