using Basket.Api.Data;
using Basket.Api.Models;
using Discount.Grpc;

namespace Basket.Api.Basket.CreateBasket;
internal sealed record StoreBasketCommand(Cart Cart) : ICommand<StoreBasketResponse>;
internal sealed record StoreBasketResponse(string UserName);

internal sealed class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(e => e.Cart.UserName)
            .NotEmpty()
            .WithMessage($"Field {nameof(Cart.UserName)} must not be empty");
    }
}


internal sealed class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketResponse>
{
    private readonly IBasketRepository _basketRepository;
    private readonly IValidator<StoreBasketCommand> _validator;
    private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;

    public StoreBasketCommandHandler(
        IValidator<StoreBasketCommand> validator,
        IBasketRepository basketRepository,
        DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)

    {
        _validator = validator;
        _basketRepository = basketRepository;
        _discountProtoServiceClient = discountProtoServiceClient;
    }

    public async Task<StoreBasketResponse> Handle(
        StoreBasketCommand command,
        CancellationToken cancellationToken)
    {
        await DeductDiscount(command.Cart, cancellationToken);

        var result = await _basketRepository.StoreBasketAsync(command.Cart);

        return new StoreBasketResponse($"{result.UserName} - cart successfully created");
    }

    private async Task DeductDiscount(Cart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.CartItems)
        {
            var coupon = await _discountProtoServiceClient.GetDiscountAsync(
                new GetDiscountRequest { ProductName = item.Name },
                cancellationToken: cancellationToken);

            item.Price -= coupon.Amount;
            item.IsDiscount = true;
        }
    }
}
