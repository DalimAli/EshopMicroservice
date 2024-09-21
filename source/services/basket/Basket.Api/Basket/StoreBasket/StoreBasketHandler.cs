
namespace Basket.Api.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart ShoppingCart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName); 

public class StoreBasketCommandValidator: AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.ShoppingCart)
            .NotNull()
            .WithMessage("Cart can not be empty");

        RuleFor(x => x.ShoppingCart.UserName)
            .NotEmpty()
            .WithMessage("UserName is required");
    }

}

public class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.ShoppingCart;

        //TODO: store baseket in db (kuse marten upsert - if exist then update else insert)
        //TODO: Update Cache

        return new StoreBasketResult(cart.UserName);
    }
}

