namespace Ordering.Domain.ValueObject;

public sealed record OrderId
{
    public Guid Value { get; }
}