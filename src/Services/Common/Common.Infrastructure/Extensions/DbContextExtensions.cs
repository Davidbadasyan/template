namespace Common.Infrastructure.Extensions;

public static class DbContextExtensions
{
    public static void TryMigrateDbContext<TContext>(this IHost webHost) where TContext : Microsoft.EntityFrameworkCore.DbContext
    {
        _ = Task.Factory.StartNew(async () =>
        {
            using var scope = webHost.Services.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<TContext>>();
            logger.LogInformation($"Trying migrate {typeof(TContext)}");
            await using var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();

            Policy.Handle<SqlException>()
                .WaitAndRetryForever(_ => TimeSpan.FromSeconds(5), (ex, ts) =>
                {
                    logger.LogError($"Could not migrate {typeof(TContext)}. Trying again. Exception: {ex.Message}");
                })
                .Execute(() =>
                {
                    dbContext.Database.Migrate();
                });
        });
    }
}