using Basket.Api.Models;

namespace Basket.Api.Basket.GetBasket;

public sealed record GetBasketRequest(string UserName);

internal sealed record GetBasketResult(Cart Cart);

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{username}", async (string userName, ISender send) =>
        {
            var result = await send.Send(new GetBasketQuery(userName));

            var adapt = result.Adapt<GetBasketResult>();

            return Results.Ok(adapt);
        })
        .Produces<GetBasketResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
