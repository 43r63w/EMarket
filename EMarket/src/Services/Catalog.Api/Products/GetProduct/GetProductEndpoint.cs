using Catalog.Api.Models;
using Catalog.Api.Products.GetProduct;
using Mapster;

namespace Catalog.Api.Products.GetProductById;

internal sealed record GetProductResponse(Product Product);

public sealed class GetProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("product/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetProductQuery(id));

            var response = result.Adapt<GetProductResponse>();

            return Results.Ok(response);
        })
        .Produces<GetProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
