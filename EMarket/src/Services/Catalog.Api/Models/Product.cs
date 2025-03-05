namespace Catalog.Api.Models;

public sealed class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public List<string> Categories { get; set; } = [];

    public string ImagePath { get; set; } = null!;

    public decimal Price { get; set; }
}
