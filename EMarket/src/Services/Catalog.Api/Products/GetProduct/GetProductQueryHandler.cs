using Catalog.Api.Exceptions;
using Catalog.Api.Models;
using Marten;

namespace Catalog.Api.Products.GetProduct;

internal sealed record GetProductQuery(Guid Id) : IQuery<GetProductQueryResponse>;

internal sealed record GetProductQueryResponse(Product Product);

internal sealed class GetProductQueryHandler : IQueryHandler<GetProductQuery, GetProductQueryResponse>
{
    private readonly IDocumentSession _documentSession;

    public GetProductQueryHandler(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }

    public async Task<GetProductQueryResponse> Handle(
        GetProductQuery query,
        CancellationToken cancellationToken = default)
    {
        var result = await _documentSession.LoadAsync<Product>(query.Id, cancellationToken);

        if (result is null)
            throw new ProductNotFoundException(query.Id);

        return new GetProductQueryResponse(result);
    }
}
