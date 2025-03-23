using Ordering.Domain.Abstractions;
using Ordering.Domain.ValueObject;

namespace Ordering.Domain.Models;

public class OrderItem : Entity<Guid>
{
    public OrderItem(OrderId orderId, ProductId productId, int quantity, decimal price)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public OrderId OrderId { get; private set; } = default!;
    public ProductId ProductId { get; private set; } = default!;

    public int Quantity { get; private set; } = default!;

    public decimal Price { get; private set; } = default!;
}