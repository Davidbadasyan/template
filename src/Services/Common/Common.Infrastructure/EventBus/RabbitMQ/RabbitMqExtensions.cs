namespace Common.Infrastructure.EventBus.RabbitMQ;

public static class RabbitMqExtensions
{
    public static void AddRabbitMqEventBus(
        this IServiceCollection services,
        string subscriptionClientName,
        string hostName,
        string userName,
        string password,
        int retryCount)
    {
        services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

        services.AddSingleton<IRabbitMqPersistentConnection>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<DefaultRabbitMqPersistentConnection>>();

            var factory = new ConnectionFactory()
            {
                HostName = hostName,
                DispatchConsumersAsync = true
            };

            if (!string.IsNullOrEmpty(userName))
            {
                factory.UserName = userName;
            }

            if (!string.IsNullOrEmpty(password))
            {
                factory.Password = password;
            }

            return new DefaultRabbitMqPersistentConnection(factory, logger, retryCount);
        });


        services.AddSingleton<IEventBus, EventBusRabbitMq>(sp =>
        {
            var rabbitMqPersistentConnection = sp.GetRequiredService<IRabbitMqPersistentConnection>();
            var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
            var logger = sp.GetRequiredService<ILogger<EventBusRabbitMq>>();
            var eventBusSubscriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

            return new EventBusRabbitMq(rabbitMqPersistentConnection, logger, iLifetimeScope, eventBusSubscriptionsManager, subscriptionClientName, retryCount);
        });
    }
}