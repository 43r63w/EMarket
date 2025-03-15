
using Basket.Api.Data;

namespace Basket.Api.Basket.DeleteBasket;

internal sealed record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResponse>;

internal sealed record DeleteBasketResponse(string message);

internal sealed class DeleteBasketCommandHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketResponse>
{
    private readonly IBasketRepository _basketRepository;

    public DeleteBasketCommandHandler(IBasketRepository basketRepository)
    {

        _basketRepository = basketRepository;
    }

    public async Task<DeleteBasketResponse> Handle(
        DeleteBasketCommand command,
        CancellationToken cancellationToken)
    {
        await _basketRepository.DeleteBasketAsync(command.UserName);

        return new DeleteBasketResponse($"{command.UserName} successfully deleted");
    }
}
