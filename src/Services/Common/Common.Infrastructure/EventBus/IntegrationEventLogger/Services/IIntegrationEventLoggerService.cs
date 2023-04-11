namespace Common.Infrastructure.EventBus.IntegrationEventLogger.Services;

public interface IIntegrationEventLoggerService
{
    Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId);
    Task SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction);
    Task MarkEventAsPublishedAsync(Guid eventId);
    Task MarkEventAsInProgressAsync(Guid eventId);
    Task MarkEventAsFailedAsync(Guid eventId);
}