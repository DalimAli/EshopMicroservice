using FluentValidation;
using System.Reflection.Metadata;
using System.Security.Cryptography.Xml;

namespace Catalog.Api.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {

        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .Length(2, 150)
            .WithMessage("Name must be between 2 and 150 characters");
        RuleFor(x => x.Category)
            .NotEmpty()
            .WithMessage("Category is required");
        RuleFor(x => x.ImageFile)
            .NotEmpty()
            .WithMessage("ImageFile is required");
        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0");
    }
}

internal class CreateProductCommandHandler(IDocumentSession documentSession, ILogger<CreateProductCommandHandler> logger) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    async Task<CreateProductResult> IRequestHandler<CreateProductCommand, CreateProductResult>.Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(CreateProductCommandHandler)} {nameof(Handle)}  called with @command", command);

        // create product from product command
        Product product = new()
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price,
        };

        documentSession.Store(product);
        await documentSession.SaveChangesAsync(cancellationToken);


        return new CreateProductResult(product.Id);

    }
}

