namespace Orders.Infrastructure.DbContexts;

public class OrdersQueryContext : ReadableDbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<ShippingMethod> ShippingMethods { get; set; }
    public DbSet<WeightUnit> WeightUnits { get; set; }
    public OrdersQueryContext(DbContextOptions<OrdersQueryContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OrderStatusEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentMethodEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ShippingMethodEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new WeightUnitEntityTypeConfiguration());
    }
}