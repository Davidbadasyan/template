namespace Orders.Infrastructure.DbContexts;

public class OrdersContext : WritableDbContext
{
    public DbSet<Order> Orders { get; set; }

    public OrdersContext(DbContextOptions<OrdersContext> options) : base(options) { }

    public OrdersContext(
        DbContextOptions<OrdersContext> options,
        IMediator mediator,
        ICurrentUser currentUser) : base(options, mediator, currentUser) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //enum data
        modelBuilder.Entity<OrderStatus>().HasData(OrderStatus.List());
        modelBuilder.Entity<PaymentMethod>().HasData(PaymentMethod.List());
        modelBuilder.Entity<ShippingMethod>().HasData(ShippingMethod.List());
        modelBuilder.Entity<WeightUnit>().HasData(WeightUnit.List());
    }
}