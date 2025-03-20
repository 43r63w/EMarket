using Discount.Grpc.Data;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServices(
        this IServiceCollection service, 
        IConfiguration configuration)
    {
        service.AddDbContext<DiscountDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("Database")!);
        });


        return service;
    }
}
