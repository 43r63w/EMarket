using Catalog.Api.Models;
using Mapster;
using Marten;
using Marten.Pagination;

namespace Catalog.Api.Products.GetProducts;

internal sealed record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsQueryResponse>;

internal sealed record GetProductsQueryResponse(IEnumerable<Product> Products);

internal sealed class GetProdcutsQueryHandler : IQueryHandler<GetProductsQuery, GetProductsQueryResponse>
{
    private readonly IDocumentSession _documentSession;

    public GetProdcutsQueryHandler(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }

    public async Task<GetProductsQueryResponse> Handle(
        GetProductsQuery query,
        CancellationToken cancellationToken)
    {
        var response = await _documentSession
            .Query<Product>()
            .ToPagedListAsync(
            pageNumber: query.PageNumber ?? 1,
            pageSize: query.PageSize ?? 10,
            cancellationToken);

        if (response is null)
            throw new ProductNotFoundException();

        var products = new GetProductsQueryResponse(response);

        return products;
    }
}
