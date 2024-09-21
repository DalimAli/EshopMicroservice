
namespace Basket.Api.Basket.StoreBasket;

public record StoreBasketRequest(ShoppingCart ShoppingCart);

public record StoreBasketResponse(string UserName);

public class StoreBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {

        app.MapPost("/basket",
            async (StoreBasketRequest request, ISender sender) =>
            {
                var command = request.Adapt<StoreBasketCommand>();

                StoreBasketResult? result = await sender.Send(command);

                StoreBasketResponse? response = result.Adapt<StoreBasketResponse>();

                return Results.Created($"/products/{response.UserName}", response.UserName);

            })
            .WithName("CreateBasket")
            .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Basket")
            .WithDescription("Create Basket");

    }
}
