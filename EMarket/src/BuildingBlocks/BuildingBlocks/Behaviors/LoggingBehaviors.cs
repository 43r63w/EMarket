using BuildingBlocks.CQRS;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors;
public class LoggingBehaviors<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, ICommand<TResponse>
    where TResponse : notnull
{
    private readonly ILogger<LoggingBehaviors<TRequest, TResponse>> _logger;

    public LoggingBehaviors(ILogger<LoggingBehaviors<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("[START]: Handle request = {Request} - response = {Response}",
            typeof(TRequest).Name, typeof(TResponse).Name, request);

        var timer = new Stopwatch();
        timer.Start();

        var response = await next();

        timer.Stop();

        var takenTime = timer.Elapsed;
        if (takenTime.Seconds > 3)
            _logger.LogWarning("[PERFORMANCE] The request={Request} took {Second}",
                typeof(TRequest).Name, takenTime.Seconds);

        _logger.LogInformation("[END]: Handle {Request} with {Response}",
           typeof(TRequest).Name, typeof(TResponse).Name, request);

        return response;
    }
}
