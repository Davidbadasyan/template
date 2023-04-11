namespace Orders.API.Extensions;

public static class EventBusExtensions
{
    public static void UseEventBus(this IHost host)
    {
        var eventBus = host.Services.GetRequiredService<IEventBus>();

        eventBus.Subscribe<TestIntegrationEvent, TestIntegrationEventHandler>();
    }
}