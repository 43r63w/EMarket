

namespace Ordering.Domain.ValueObject;

public sealed record Address
{
    public string? FirstName { get; } = default!;

    public string? LastName { get; } = default!;

    public string? EmailAddress { get; set; } = default!;
    public string? AddressLine { get; set; } = default!;

    public string? Country { get; set; } = default!;

    public string? State { get; set; } = default!;

    public string? ZipCode { get; set; } = default!;
}