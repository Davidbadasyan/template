namespace UM.Infrastructure.EntityConfigurations;

public class UserRoleEntityConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> userRoleConfiguration)
    {
        userRoleConfiguration.ToBaseEnumerationConfig();
    }
}