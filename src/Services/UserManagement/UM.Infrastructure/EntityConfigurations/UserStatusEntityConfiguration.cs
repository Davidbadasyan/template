namespace UM.Infrastructure.EntityConfigurations;

public class UserStatusEntityConfiguration : IEntityTypeConfiguration<UserStatus>
{
    public void Configure(EntityTypeBuilder<UserStatus> userStatusConfiguration)
    {
        userStatusConfiguration.ToBaseEnumerationConfig();
    }
}