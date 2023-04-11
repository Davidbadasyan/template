namespace Orders.Infrastructure.EntityConfigurations;

public class WeightUnitEntityTypeConfiguration : IEntityTypeConfiguration<WeightUnit>
{
    public void Configure(EntityTypeBuilder<WeightUnit> builder)
    {
        builder.ToBaseEnumerationConfig();
    }
}