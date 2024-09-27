﻿using Discount.Grpc;

namespace Basket.Api.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart ShoppingCart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
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

public class StoreBasketCommandHandler
    (IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await DeductDiscount(command.ShoppingCart, cancellationToken);

        await basketRepository.StoreBasket(command.ShoppingCart);

        return new StoreBasketResult(command.ShoppingCart.UserName);
    }

    private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var coupon = await discountProtoServiceClient.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
            item.Price -= coupon.Amount;
        }
    }
}

