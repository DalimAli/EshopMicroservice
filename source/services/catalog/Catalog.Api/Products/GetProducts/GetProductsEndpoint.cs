
using OpenTelemetry.Trace;

namespace Catalog.Api.Products.GetProducts;

public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);
public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender serder) =>
        {
            var query = request.Adapt<GetProductsQuery>();

            var result = await serder.Send(query);

            var response = result.Adapt<GetProductsResponse>();

            return Results.Ok(response);
        })
            .WithName("gets")
            .Produces<GetProductsResponse>(statusCode: StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Get all Products");
    }
}

