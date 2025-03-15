using Basket.Api.Exception;
using Basket.Api.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Api.Data;

public class BasketRepository : IBasketRepository
{
    private readonly IDocumentSession _documentSession;
    private readonly IDistributedCache _distributedCache;

    public BasketRepository(
        IDocumentSession documentSession,
        IDistributedCache distributedCache)
    {
        _documentSession = documentSession;
        _distributedCache = distributedCache;
    }

    public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        _documentSession.Delete<Cart>(userName);
        await _distributedCache.RemoveAsync(userName,cancellationToken);
        await _documentSession.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<Cart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        var getBasket = await _distributedCache.GetStringAsync(userName, cancellationToken);
        if (!string.IsNullOrEmpty(getBasket))
            return JsonSerializer.Deserialize<Cart>(getBasket);


        var basket = await _documentSession.LoadAsync<Cart>(userName, cancellationToken);

        await _distributedCache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);

        return basket is null ? throw new BasketNotFoundException(userName) : basket;
    }

    public async Task<Cart> StoreBasketAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        _documentSession.Store<Cart>(cart);
        await _documentSession.SaveChangesAsync(cancellationToken);

        await _distributedCache.SetStringAsync(cart.UserName, JsonSerializer.Serialize(cart), cancellationToken);

        return cart;
    }
}
