using Basket.Api.Basket.CreateBasket;
using Basket.Api.Data;
using Basket.Api.Models;
using Discount.Grpc;
using System.Reflection;

namespace Basket.Api;

public static class ServicesExetention
{
    public const string DATABASE = "Database";
    public const string REDIS = "Redis";
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
            options.Configuration = configuration.GetConnectionString(REDIS)!;
            options.InstanceName = "Basket";
        });

        return services;
    }

    public static IServiceCollection AddHealthChecker(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString(DATABASE)!)
            .AddRedis(REDIS); 

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(DiscountProtoService.DiscountProtoServiceClient));
        return services;
    }

    public static IServiceCollection AddGrpcClient(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
        {
            options.Address = new Uri(configuration["GrpcSettings:DiscountUrl"]!);
        });

        return services;
    }

}
