using Basket.Api.Data;
using Basket.Api.Models;

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

    public StoreBasketCommandHandler(
        IValidator<StoreBasketCommand> validator,
        IBasketRepository basketRepository)
    {
        _validator = validator;
        _basketRepository = basketRepository;
    }

    public async Task<StoreBasketResponse> Handle(
        StoreBasketCommand command,
        CancellationToken cancellationToken)
    {

        var result = await _basketRepository.StoreBasketAsync(command.Cart);

        return new StoreBasketResponse($"{result.UserName} - cart successfully created");
    }
}
