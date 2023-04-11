namespace Common.Infrastructure.DbContext;

public abstract class ReadableDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    protected ReadableDbContext(DbContextOptions options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
}