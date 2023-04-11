using Common.Application;

namespace UM.Infrastructure.DbContexts;

public class UmContext : WritableDbContext
{
    public DbSet<User> Users { get; set; }

    public UmContext(DbContextOptions<UmContext> options) : base(options) { }

    public UmContext(
        DbContextOptions<UmContext> options,
        IMediator mediator,
        ICurrentUser currentUser) : base(options, mediator, currentUser) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //enum data
        modelBuilder.Entity<UserStatus>().HasData(UserStatus.List());
        modelBuilder.Entity<UserRole>().HasData(UserRole.List());
    }
}