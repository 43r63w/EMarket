namespace Basket.Api.Models;

public class Item
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public int Quantity { get; set; }

    public string Color { get; set; } = default!;

    public decimal Price { get; set; }
}