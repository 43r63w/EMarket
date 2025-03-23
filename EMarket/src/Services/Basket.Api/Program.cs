using Basket.Api;
using Basket.Api.Models;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handlres;
using HealthChecks.UI.Client;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMarten(cfg =>
{
    cfg.Connection(builder.Configuration.GetConnectionString("Database")!);
    cfg.Schema.For<Cart>().Identity(e => e.UserName);
}).UseIdentitySessions();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

    config.AddOpenBehavior(typeof(LoggingBehaviors<,>));
    config.AddOpenBehavior(typeof(ValidationBehaviors<,>));
});


builder.Services.AddRedis(builder.Configuration);

builder.Services.AddCarter();

builder.Services.AddValidaitonSchema();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecker(builder.Configuration);

builder.Services.AddGrpcClient(builder.Configuration);


builder.Services.AddOpenApi();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseExceptionHandler(opt => { });

app.MapCarter();

app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();


