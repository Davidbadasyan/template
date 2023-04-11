namespace UM.Application.IntegrationEventHandlers;

public class TestIntegrationEventHandler : IIntegrationEventHandler<TestIntegrationEvent>
{
    private readonly ILogger<TestIntegrationEventHandler> _logger;
    public TestIntegrationEventHandler(ILogger<TestIntegrationEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TestIntegrationEvent @event)
    {
        _logger.LogInformation("---- HANDLING TEST INTEGRATION EVENT FROM UM");
        return Task.CompletedTask;
    }
}