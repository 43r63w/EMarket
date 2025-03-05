
using Mapster;

namespace Catalog.Api.Products.UpdateProduct;

internal sealed record UpdateProductRequest(Guid Id,
    string? Name,
    string? Description,
    List<string>? Categories,
    string? ImagePath,
    decimal? Price);

internal sealed record UpdateProductResponse(Guid Id);

public sealed class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("update/{Id}", async (
            UpdateProductRequest updateProductRequest,
            ISender sender) =>
        {
            var map = updateProductRequest.Adapt<UpdateProductCommand>();
            var result = await sender.Send(map);

            var response = result.Adapt<UpdateProductResponse>();

            return Results.Ok(response);
        })
        .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
