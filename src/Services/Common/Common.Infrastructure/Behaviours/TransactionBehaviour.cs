namespace Common.Infrastructure.Behaviours;

public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly WritableDbContext _dbContext;
    private readonly IIntegrationEventService _integrationEventService;

    public TransactionBehaviour(
        WritableDbContext dbContext,
        IIntegrationEventService integrationEventService)
    {
        _dbContext = dbContext ?? throw new ArgumentException(nameof(WritableDbContext));
        _integrationEventService = integrationEventService ?? throw new ArgumentException(nameof(IIntegrationEventService));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var response = default(TResponse);

        try
        {
            if (_dbContext.HasActiveTransaction)
            {
                return await next();
            }

            var strategy = _dbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                Guid transactionId;

                await using (var transaction = await _dbContext.BeginTransactionAsync())
                {
                    response = await next();

                    await _dbContext.CommitTransactionAsync(transaction);

                    transactionId = transaction.TransactionId;
                }

                await _integrationEventService.PublishEventsThroughEventBusAsync(transactionId);
            });

            return response;
        }
        catch
        {
            throw;
        }
    }
}