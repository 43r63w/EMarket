using Discount.Grpc.Data;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.RequestPipline;

public static class WebApplicationExtensions
{
    public static async Task<IApplicationBuilder> InitializeDbAsync(this IApplicationBuilder app)
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
