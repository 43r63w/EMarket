using Basket.Api.Basket.CreateBasket;
using Basket.Api.Models;

namespace Basket.Api.Basket.StoreBasket;

internal sealed record PostBasketRequest(Cart Cart);

public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (PostBasketRequest request, ISender send) =>
        {
            var adapt = request.Adapt<StoreBasketCommand>();

            var result = await send.Send(adapt);

            return Results.Created($"/basket/{result.UserName}", result);
        })
        .Produces(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
