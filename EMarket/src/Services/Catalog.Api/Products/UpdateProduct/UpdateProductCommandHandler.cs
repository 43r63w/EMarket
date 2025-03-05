using Catalog.Api.Models;

namespace Catalog.Api.Products.UpdateProduct;

internal sealed record UpdateProductCommand(
    Guid Id,
    string? Name,
    string? Description,
    List<string>? Categories,
    string? ImagePath,
    decimal? Price) : ICommand<UpdateCommandResult>;

internal sealed record UpdateCommandResult(Guid Id);


internal sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, UpdateCommandResult>
{
    private readonly IDocumentSession _documentSession;

    public UpdateProductCommandHandler(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }

    public async Task<UpdateCommandResult> Handle(
        UpdateProductCommand command,
        CancellationToken cancellationToken = default)
    {
        var product = await _documentSession.LoadAsync<Product>(command.Id);

        if (product is null)
            throw new ProductNotFoundException(command.Id);

        product.Name = command.Name ?? product.Name;
        product.Description = command.Description ?? product.Description;
        product.Categories = command.Categories ?? product.Categories;
        product.ImagePath = command.ImagePath ?? product.ImagePath;
        product.Price = command.Price ?? product.Price;

        _documentSession.Update<Product>(product);

        await _documentSession.SaveChangesAsync();

        return new UpdateCommandResult(product.Id);

    }
}
