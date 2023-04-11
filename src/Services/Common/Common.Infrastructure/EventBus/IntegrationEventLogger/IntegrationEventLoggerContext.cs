namespace Common.Infrastructure.EventBus.IntegrationEventLogger;

public class IntegrationEventLoggerContext : Microsoft.EntityFrameworkCore.DbContext
{
    public IntegrationEventLoggerContext(DbContextOptions<IntegrationEventLoggerContext> options) : base(options)
    {
    }

    public DbSet<IntegrationEventLogEntry> IntegrationEventLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<IntegrationEventLogEntry>(ConfigureIntegrationEventLogEntry);
        builder.Entity<EventState>(ConfigureEventState);

        builder.Entity<EventState>().HasData(EventState.List());
    }

    private void ConfigureIntegrationEventLogEntry(EntityTypeBuilder<IntegrationEventLogEntry> builder)
    {
        builder.ToTable("integration_event_logs");

        builder.HasKey(e => e.EventId);

        builder.Property(e => e.EventId)
            .HasColumnName("event_id")
            .IsRequired();

        builder.Property(e => e.Content)
            .HasColumnName("content")
            .IsRequired();

        builder.Property(e => e.TransactionId)
            .HasColumnName("transaction_id")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(e => e.CreationTime)
            .HasColumnName("creation_time")
            .IsRequired();

        builder.Property<int>("_stateId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("state_id")
            .IsRequired();

        builder.HasOne(e => e.State)
            .WithMany()
            .HasForeignKey("_stateId");

        builder.Property(e => e.TimesSent)
            .HasColumnName("times_sent")
            .IsRequired();

        builder.Property(e => e.EventTypeName)
            .HasColumnName("event_type_name")
            .IsRequired();
    }

    private void ConfigureEventState(EntityTypeBuilder<EventState> builder)
    {
        builder.ToTable("integration_event_states");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .HasDefaultValue(1)
            .ValueGeneratedNever()
            .HasColumnName("id")
            .IsRequired();

        builder.Property(o => o.Name)
            .HasMaxLength(200)
            .HasColumnName("name")
            .IsRequired();
    }
}