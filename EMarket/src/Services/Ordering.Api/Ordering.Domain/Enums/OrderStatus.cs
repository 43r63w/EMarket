namespace Ordering.Domain.Enums;

public enum OrderStatus
{
    Pending = 1,
    Cancelled = 2,
    Shipped = 3,
    ReadyToDelive = 4,
    NeedToPay = 5,
    Completed = 6,
    MoneyBack = 7,
    Draft = 8
}
