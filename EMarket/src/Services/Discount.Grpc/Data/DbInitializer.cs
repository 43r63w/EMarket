using Microsoft.EntityFrameworkCore;
namespace Discount.Grpc.Data;
internal static class DbInitializer
{
    public static async Task<IApplicationBuilder> MigrateAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountDbContext>();

        var pendingMigration = await dbContext.Database.GetPendingMigrationsAsync();  
        if (pendingMigration.Any())
        {
            await dbContext.Database.MigrateAsync();
        }

        return app;
    }
}
