using Basket.Api.Basket.CreateBasket;
using Basket.Api.Data;
using Basket.Api.Models;
using System.Reflection;

namespace Basket.Api;

public static class ServicesExetention
{
    public const string DATABASE = "Database";
    public static Assembly ASSEMBLY = typeof(ServicesExetention).Assembly;
    public static IServiceCollection AddMarten(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMarten(cfg =>
        {
            cfg.Connection(configuration.GetConnectionString(DATABASE)!);
            cfg.Schema.For<Cart>().Identity(e => e.UserName);
        })
        .UseLightweightSessions();
        return services;
    }

    public static IServiceCollection AddValidaitonSchema(this IServiceCollection services)
    {
        services.AddTransient<IValidator<StoreBasketCommand>, StoreBasketCommandValidator>();

        services.AddValidatorsFromAssemblyContaining<StoreBasketCommandValidator>();

        return services;
    }

    public static IServiceCollection AddRedis(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis")!;
            options.InstanceName = "Basket";
        });

        return services;
    }
}
