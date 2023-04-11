namespace Common.Infrastructure.EventBus.IntegrationEventLogger.Services;

public class IntegrationEventLoggerService : IIntegrationEventLoggerService, IDisposable
{
    private readonly IntegrationEventLoggerContext _integrationEventLoggerContext;
    private readonly List<Type> _eventTypes;
    private volatile bool _disposedValue;

    public IntegrationEventLoggerService(
        DbConnection dbConnection,
        List<Type> eventTypes)
    {
        _integrationEventLoggerContext = new IntegrationEventLoggerContext(
            new DbContextOptionsBuilder<IntegrationEventLoggerContext>()
                .UseSqlServer(dbConnection)
                .Options);

        _eventTypes = eventTypes;
    }

    public async Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId)
    {
        var tid = transactionId.ToString();

        var result = await _integrationEventLoggerContext
            .IntegrationEventLogs
            .Include(e => e.State)
            .Where(e => e.TransactionId == tid && e.State == EventState.NotPublished).ToListAsync();

        if (result.Any())
        {
            return result
                .OrderBy(o => o.CreationTime)
                .Select(e => e.DeserializeJsonContent(_eventTypes.Find(t => t.Name == e.EventTypeShortName)));
        }

        return new List<IntegrationEventLogEntry>();
    }

    public Task SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));

        var eventLogEntry = new IntegrationEventLogEntry(@event, transaction.TransactionId);

        _integrationEventLoggerContext.Database.UseTransaction(transaction.GetDbTransaction());
        _integrationEventLoggerContext.IntegrationEventLogs.Add(eventLogEntry);

        return _integrationEventLoggerContext.SaveChangesAsync();
    }

    public Task MarkEventAsPublishedAsync(Guid eventId)
    {
        return UpdateEventStatus(eventId, EventState.Published);
    }

    public Task MarkEventAsInProgressAsync(Guid eventId)
    {
        return UpdateEventStatus(eventId, EventState.InProgress);
    }

    public Task MarkEventAsFailedAsync(Guid eventId)
    {
        return UpdateEventStatus(eventId, EventState.PublishedFailed);
    }

    private Task UpdateEventStatus(Guid eventId, EventState status)
    {
        var eventLogEntry = _integrationEventLoggerContext.IntegrationEventLogs
            .Include(x => x.State)
            .Single(ie => ie.EventId == eventId);

        eventLogEntry.SetState(status);

        if (status == EventState.InProgress)
            eventLogEntry.TimesSent++;

        _integrationEventLoggerContext.IntegrationEventLogs.Update(eventLogEntry);

        return _integrationEventLoggerContext.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _integrationEventLoggerContext?.Dispose();
            }


            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}