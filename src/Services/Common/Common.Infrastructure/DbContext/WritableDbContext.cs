namespace Common.Infrastructure.DbContext;

public abstract class WritableDbContext : Microsoft.EntityFrameworkCore.DbContext, IUnitOfWork
{
    private readonly IMediator _mediator;
    private IDbContextTransaction? _currentTransaction;
    private readonly ICurrentUser _currentUser;

    protected WritableDbContext() { }

    protected WritableDbContext(DbContextOptions options) : base(options) { }

    protected WritableDbContext(
        DbContextOptions options,
        IMediator mediator,
        ICurrentUser currentUser) : base(options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    public IDbContextTransaction? GetCurrentTransaction() => _currentTransaction;

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;

        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction)
            throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public bool HasActiveTransaction => _currentTransaction != null;

    public virtual async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        // Dispatch Domain Events collection. 
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
        await _mediator.DispatchDomainEventsAsync(this);

        var addedEntries = ChangeTracker.Entries().Where(p => p.State == EntityState.Added).ToList();
        var modifiedEntries = ChangeTracker.Entries().Where(p => p.State == EntityState.Modified).ToList();

        SetCreators(addedEntries, _currentUser.Email);
        SetModifiers(modifiedEntries, _currentUser.Email);

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        var result = await base.SaveChangesAsync(cancellationToken);

        return true;
    }

    private void SetCreators(List<EntityEntry> entityEntries, string currentUserEmail)
    {
        if (string.IsNullOrWhiteSpace(currentUserEmail))
            return;

        var newEntries = entityEntries.FindAll(x => x.Entity.GetType().GetInterfaces().Contains(typeof(ICreator)));
        if (newEntries.Any())
        {
            newEntries.ForEach(e => (e.Entity as ICreator)?.SetCreator(currentUserEmail, DateTimeOffset.UtcNow));
        }
    }

    private void SetModifiers(List<EntityEntry> entityEntries, string currentUserEmail)
    {
        if (string.IsNullOrWhiteSpace(currentUserEmail))
            return;

        var modifiedEntries = entityEntries.FindAll(x => x.Entity.GetType().GetInterfaces().Contains(typeof(IModifier)));
        if (modifiedEntries.Any())
        {
            modifiedEntries.ForEach(e => (e.Entity as IModifier)?.SetModifier(currentUserEmail, DateTimeOffset.UtcNow));
        }
    }
}
