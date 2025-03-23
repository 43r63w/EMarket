using Ordering.Domain.Abstractions;
using Ordering.Domain.Enums;
using Ordering.Domain.ValueObject;

namespace Ordering.Domain.Models;

public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = new();

    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public CustomerId CustomerId { get; private set; } = default!;

    public string OrderName { get; private set; } = default!;

    public Address BillingAddress { get; set; }

    public Address ShippingAddress { get; set; }

    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

    public decimal TotalPrice
    {
        get => OrderItems.Sum(e => e.Price * e.Quantity);
        private set { }
    }
}
