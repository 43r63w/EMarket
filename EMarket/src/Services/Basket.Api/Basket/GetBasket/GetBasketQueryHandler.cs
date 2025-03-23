
using Basket.Api.Data;
using Basket.Api.Models;
using Discount.Grpc;


namespace Basket.Api.Basket.GetBasket;

internal sealed record GetBasketQuery(string UserName) : IQuery<GetBasketResponse>;

internal sealed record GetBasketResponse(Cart Cart);


internal sealed class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetBasketResponse>
{
    private readonly IBasketRepository _basketRepository;
    private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;
    public GetBasketQueryHandler(
        IBasketRepository basketRepository,
        DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
    {
        _basketRepository = basketRepository;
        _discountProtoServiceClient = discountProtoServiceClient;
    }

    public async Task<GetBasketResponse> Handle(
        GetBasketQuery query,
        CancellationToken cancellationToken)
    {
        var cart = await _basketRepository.GetBasketAsync(query.UserName);

        foreach (var item in cart.CartItems)
        {
            var coupon = await _discountProtoServiceClient.GetDiscountAsync(new GetDiscountRequest
            { ProductName = item.Name }, cancellationToken: cancellationToken);

            item.Price -= coupon.Amount;
            item.IsDiscount = true;
        }

   

        if (cart is null)
            throw new NotFoundException($"{query.UserName} does`nt have cart");

        return new GetBasketResponse(cart);
    }
}
