namespace UM.Infrastructure.DbContexts;

public class UmQueryContext : ReadableDbContext
{
    public DbSet<User> Users { get; set; }

    public UmQueryContext(DbContextOptions<UmQueryContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}