namespace Basket.Api.Models;

public class Cart
{
    public string UserName { get; set; }

    public List<Item> CartItems { get; set; }

    public decimal TotalPrice =>  CartItems?.Sum(e => e.Price * e.Quantity) ?? 0;

    public Cart(string userName)
    {
        UserName = userName;
    }

    public Cart()
    {
        
    }
}
