namespace UM.Infrastructure.EntityConfigurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToBaseEntityConfig(includeUsers: false);

        builder
            .Property(u => u.Email)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("email")
            .HasMaxLength(250)
            .IsRequired();

        builder
            .HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.FirstName)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("first_name")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(u => u.LastName)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("last_name")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property<int>("_statusId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("approval_status_id")
            .IsRequired();

        builder.HasOne(u => u.Status)
            .WithMany()
            .HasForeignKey("_statusId");

        builder.Property<int>("_roleId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("role_id")
            .IsRequired();

        builder.HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey("_roleId");
    }
}