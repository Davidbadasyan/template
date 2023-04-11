namespace Common.Infrastructure.EventBus.IntegrationEventLogger.Services;

public class IntegrationEventService : IIntegrationEventService
{
    private readonly IEventBus _eventBus;
    private readonly WritableDbContext _context;
    private readonly IIntegrationEventLoggerService _integrationEventLoggerService;
    private readonly ILogger<IntegrationEventService> _logger;

    public IntegrationEventService(
        IEventBus eventBus,
        WritableDbContext context,
        ILogger<IntegrationEventService> logger)
    {
        _eventBus = eventBus;
        _context = context;
        _logger = logger;

        _integrationEventLoggerService = new IntegrationEventLoggerService(
            dbConnection: _context.Database.GetDbConnection(),
            eventTypes: typeof(BaseCommand).GetTypeInfo().Assembly
                .GetTypes()
                .Where(t => t.Name.EndsWith(nameof(IntegrationEvent)))
                .ToList());
    }

    public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
    {
        var pendingLogEvents = await _integrationEventLoggerService.RetrieveEventLogsPendingToPublishAsync(transactionId);

        foreach (var logEvt in pendingLogEvents)
        {
            _logger.LogInformation("----- Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", logEvt.EventId, logEvt.IntegrationEvent);

            try
            {
                await _integrationEventLoggerService.MarkEventAsInProgressAsync(logEvt.EventId);
                _eventBus.Publish(logEvt.IntegrationEvent);
                await _integrationEventLoggerService.MarkEventAsPublishedAsync(logEvt.EventId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR publishing integration event: {IntegrationEventId}", logEvt.EventId);

                await _integrationEventLoggerService.MarkEventAsFailedAsync(logEvt.EventId);
            }
        }
    }

    public async Task AddAndSaveEventAsync(IntegrationEvent evt)
    {
        _logger.LogInformation("----- Enqueuing integration event {IntegrationEventId} to repository ({@IntegrationEvent})", evt.Id, evt);

        await _integrationEventLoggerService.SaveEventAsync(evt, _context.GetCurrentTransaction());
    }
}
