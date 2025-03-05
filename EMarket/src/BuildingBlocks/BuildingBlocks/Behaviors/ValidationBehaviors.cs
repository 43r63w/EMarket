using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors;
public class ValidationBehaviors<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<ValidationBehaviors<TRequest, TResponse>> _logger;

    public ValidationBehaviors(IEnumerable<IValidator<TRequest>> validators,
        ILogger<ValidationBehaviors<TRequest, TResponse>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"{typeof(ValidationBehaviors<TRequest, TResponse>)}.{nameof(Handle)}.works");
        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(_validators.Select(e => e.ValidateAsync(request, cancellationToken)));

        var failures = validationResults
            .Where(e => e.Errors.Any())
            .SelectMany(e => e.Errors)
            .ToList();

        if (failures.Any())
            throw new ValidationException(failures);

        return await next();
    }
}
