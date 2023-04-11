namespace Common.Infrastructure.Repositories;

public class QueryRepository<TEntity, TKey>
    where TEntity : Entity
    where TKey : struct, IComparable
{
    private readonly ReadableDbContext _context;

    public QueryRepository(ReadableDbContext context)
    {
        _context = context;
    }

    public virtual async Task<TEntity?> GetByIdAsync(
        TKey id,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool enableTracking = false)
    {
        var query = BuildQuery(include);
        return await query.SingleOrDefaultAsync(r => r.Id.Equals(id));
    }

    private IQueryable<TEntity> BuildQuery(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        var query = _context.Set<TEntity>().AsQueryable();
        if (include != null)
        {
            query = include(query);
        }

        return query;
    }
}