using Ordering.Domain.Models;
namespace Ordering.Domain.ValueObject;

public sealed record CustomerId
{
    public Guid Value { get; }
}
