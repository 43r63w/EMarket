using Basket.Api;
using Basket.Api.Models;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handlres;
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

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseExceptionHandler(opt => { });

app.MapCarter();


app.Run();


