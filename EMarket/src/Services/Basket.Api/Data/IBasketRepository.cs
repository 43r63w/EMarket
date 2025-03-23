using Basket.Api.Models;

namespace Basket.Api.Data;

public interface IBasketRepository
{
    Task<Cart> GetBasketAsync(string userName,CancellationToken cancellationToken = default);
    Task<Cart> StoreBasketAsync(Cart cart,CancellationToken cancellationToken = default);
    Task<bool> DeleteBasketAsync(string userName,CancellationToken cancellationToken = default);


}
