using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Products.GetProducts;

internal sealed record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);

internal sealed record GetProductsResult(IEnumerable<Product> Products);

public sealed class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetProductsQuery>();

            var result = await sender.Send(query);

            var response = result.Adapt<GetProductsResult>();

            return Results.Ok(response);
        });
    }


      
}




