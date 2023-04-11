namespace Orders.Infrastructure.EntityConfigurations;

public class ShippingMethodEntityTypeConfiguration : IEntityTypeConfiguration<ShippingMethod>
{
    public void Configure(EntityTypeBuilder<ShippingMethod> builder)
    {
        builder.ToBaseEnumerationConfig();
    }
}