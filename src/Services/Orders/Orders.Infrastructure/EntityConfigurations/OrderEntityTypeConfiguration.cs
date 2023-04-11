namespace Orders.Infrastructure.EntityConfigurations;

public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToBaseEntityConfig();

        builder
            .HasIndex(o => o.Number)
            .IsUnique(true);

        builder
            .Property(o => o.Number)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("number")
            .HasMaxLength(250)
            .IsRequired(true);

        builder
            .Property(o => o.Buyer)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("buyer")
            .HasMaxLength(250)
            .IsRequired(true);

        builder
            .Property<int>("_statusId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("status_id")
            .IsRequired();

        builder.HasOne(o => o.Status)
            .WithMany()
            .HasForeignKey("_statusId");

        builder
            .Property<int>("_paymentMethodId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("payment_method_id")
            .IsRequired(true);

        builder.HasOne(o => o.PaymentMethod)
            .WithMany()
            .HasForeignKey("_paymentMethodId");

        builder
            .Property<int>("_shippingMethodId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("shipping_method_id")
            .IsRequired(true);

        builder.HasOne(o => o.ShippingMethod)
            .WithMany()
            .HasForeignKey("_shippingMethodId");

        builder
            .Property(o => o.Weight)
            .HasColumnName("weight")
            .IsRequired(true);

        builder
            .Property<int>("_weightUnitId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("weight_unit_id")
            .IsRequired(true);

        builder.HasOne(o => o.WeightUnit)
            .WithMany()
            .HasForeignKey("_weightUnitId");

        builder
            .Property(o => o.Description)
            .HasColumnName("description")
            .HasMaxLength(500)
            .IsRequired(false);

        builder
            .Property(o => o.IsDraft)
            .HasColumnName("is_draft")
            .IsRequired(true);

        builder.HasMany(o => o.Items);

        var navigation = builder.Metadata.FindNavigation(nameof(Order.Items));
        navigation.SetPropertyAccessMode(PropertyAccessMode.Field);


        //Address value object persisted as owned entity type supported since EF Core 2.0
        builder
            .OwnsOne(o => o.Address, a =>
            {
                a.Property(o => o.Street)
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .HasMaxLength(250)
                    .HasColumnName("street")
                    .IsRequired(false);

                a.Property(o => o.City)
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .HasMaxLength(250)
                    .HasColumnName("city")
                    .IsRequired(false);

                a.Property(o => o.State)
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .HasMaxLength(250)
                    .HasColumnName("state")
                    .IsRequired(false);

                a.Property(o => o.Country)
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .HasMaxLength(250)
                    .HasColumnName("country")
                    .IsRequired(false);

                a.Property(o => o.ZipCode)
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .HasMaxLength(250)
                    .HasColumnName("zip_code")
                    .IsRequired(false);

                a.WithOwner();
            });
    }
}
