namespace Orders.Infrastructure.EntityConfigurations;

public class OrderStatusEntityTypeConfiguration : IEntityTypeConfiguration<OrderStatus>
{
    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        builder.ToBaseEnumerationConfig();
    }
}