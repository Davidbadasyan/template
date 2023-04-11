namespace Orders.Infrastructure.EntityConfigurations;

public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToBaseEntityConfig();

        builder
            .Property("_orderId")
            .HasColumnName("order_id")
            .IsRequired();

        builder
            .HasOne(b => b.Order)
            .WithMany(b => b.Items)
            .HasForeignKey("_orderId");

        builder
            .Property(b => b.ProductName)
            .HasColumnName("product_name")
            .HasMaxLength(250)
            .IsRequired();

        builder
            .Property(b => b.UnitPrice)
            .HasColumnName("unit_price")
            .IsRequired();

        builder
            .Property(b => b.Discount)
            .HasColumnName("discount")
            .IsRequired();

        builder
            .Property(b => b.Units)
            .HasColumnName("units")
            .IsRequired();
    }
}