using BuildingBlocks.CQRS;
using Catalog.API.Models;
using MediatR;

namespace Catalog.Api.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);


internal class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    async Task<CreateProductResult> IRequestHandler<CreateProductCommand, CreateProductResult>.Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // create product from product command
        Product product = new()
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price,
        };

        // TODO
        // save in the database
        // for now on skip this part


        return new CreateProductResult(Guid.NewGuid());

    }
}

