namespace Common.Infrastructure.EventBus.IntegrationEventLogger;

public class IntegrationEventLogEntry
{
    private IntegrationEventLogEntry() { }
    public IntegrationEventLogEntry(IntegrationEvent @event, Guid transactionId)
    {
        EventId = @event.Id;
        CreationTime = @event.CreationDate;
        EventTypeName = @event.GetType().FullName;
        Content = JsonSerializer.Serialize(@event, @event.GetType(), new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        });
        _stateId = EventState.NotPublished.Id;
        TimesSent = 0;
        TransactionId = transactionId.ToString();
    }
    public Guid EventId { get; private set; }
    public string EventTypeName { get; private set; }
    [NotMapped]
    public string EventTypeShortName => EventTypeName.Split('.')?.Last();
    [NotMapped]
    public IntegrationEvent IntegrationEvent { get; private set; }
    public EventState State { get; private set; }
    private int _stateId;

    public void SetState(EventState state)
    {
        _stateId = state.Id;
    }
    public int TimesSent { get; set; }
    public DateTime CreationTime { get; private set; }
    public string Content { get; private set; }
    public string TransactionId { get; private set; }

    public IntegrationEventLogEntry DeserializeJsonContent(Type type)
    {
        try
        {
            IntegrationEvent = JsonSerializer.Deserialize(Content, type, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true, WriteIndented = true }) as IntegrationEvent;
            return this;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}