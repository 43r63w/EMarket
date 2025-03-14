
using Basket.Api.Data;
using Basket.Api.Models;

namespace Basket.Api.Basket.GetBasket;

internal sealed record GetBasketQuery(string UserName) : IQuery<GetBasketResponse>;

internal sealed record GetBasketResponse(Cart Cart);

internal sealed class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetBasketResponse>
{    
    private readonly IBasketRepository _basketRepository;
    public GetBasketQueryHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task<GetBasketResponse> Handle(
        GetBasketQuery query,
        CancellationToken cancellationToken)
    {
        var cart = await _basketRepository.GetBasketAsync(query.UserName);
         
        if (cart is null)
            throw new NotFoundException($"{query.UserName} does`nt have cart");

        return new GetBasketResponse(cart);
    }
}
