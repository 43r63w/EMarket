
namespace Basket.Api.Basket.DeleteBasket;


public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}", async (string userName, ISender send) =>
        {

            var response = await send.Send(new DeleteBasketCommand(userName));

            return Results.Ok(response);

        })
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
