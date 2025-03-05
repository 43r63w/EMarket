using Mapster;
using MediatR;

namespace Catalog.Api.Products.CreateProduct;

internal sealed record CreateProductRequest(string Name,
    string Description,
    List<string> Categories,
    string? ImagePath,
    decimal Price);

internal sealed record CreateProductResult(Guid Id);


public sealed class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("product", async (
            CreateProductRequest createProductRequest,
            ISender sender,
            ILogger<CreateProductEndpoint> logger) =>
        {
            logger.LogInformation($"{nameof(CreateProductEndpoint)}.works");

            var createProduct = createProductRequest.Adapt<CreateProductCommand>();

            var result = await sender.Send(createProduct);

            return Results.Ok(result);
        });
    }
}
