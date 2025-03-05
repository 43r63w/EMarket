namespace Catalog.Api.Products.CreateProduct;

internal sealed record CreateProductCommand(
    string Name,
    string Description,
    List<string> Categories,
    string? ImagePath,
    decimal Price) : ICommand<CreateProductResponse>;

internal sealed record CreateProductResponse(Guid Id);

internal sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(e => e.Name).NotEmpty().WithMessage("Name is required parameters");
        RuleFor(e => e.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

internal sealed class CreateProductCommandHandler
    : ICommandHandler<CreateProductCommand, CreateProductResponse>
{
    private readonly IValidator<CreateProductCommand> _validator;
    private readonly IDocumentSession _document;

    public CreateProductCommandHandler(
        IDocumentSession document, 
        IValidator<CreateProductCommand> validator)
    {
        _document = document;
        _validator = validator;
    }

    public async Task<CreateProductResponse> Handle(
        CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
            Categories = command.Categories,
            ImagePath = command.ImagePath,
            Price = command.Price,
        };

        _document.Store(product);

        await _document.SaveChangesAsync(cancellationToken);

        return new CreateProductResponse(product.Id);
    }
}
