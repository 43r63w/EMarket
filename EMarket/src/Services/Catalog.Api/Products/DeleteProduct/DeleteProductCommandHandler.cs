
namespace Catalog.Api.Products.DeleteProduct;

internal sealed record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

internal sealed record DeleteProductResult(Guid Id);


internal sealed class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    private readonly IDocumentSession _documentSession;

    public DeleteProductCommandHandler(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }

    public async Task<DeleteProductResult> Handle(
        DeleteProductCommand command,
        CancellationToken cancellationToken = default)
    {
        _documentSession.Delete<Product>(command.Id);
        await _documentSession.SaveChangesAsync();

        return new DeleteProductResult(command.Id);
    }
}
