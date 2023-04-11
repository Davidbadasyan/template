namespace Orders.Infrastructure.Repositories.Queries;

public class OrderQueryRepository : IOrderQueryRepository
{
    private readonly OrdersQueryContext _context;

    public OrderQueryRepository(OrdersQueryContext context)
    {
        _context = context;
    }

    public async Task<Order?> GetByIdAsync(long id, string buyer)
    {
        var order = await GetQueryableOrders()
            .FirstOrDefaultAsync(o => o.Id == id &&
                                      o.Buyer.Equals(buyer));

        return order;
    }

    public async Task<PaginatedResult<Order>> GetPaginatedAsync(
        string buyer,
        int pageNumber,
        int pageSize)
    {
        var paginatedOrders = await GetQueryableOrders()
            .Where(o => o.Buyer.Equals(buyer))
            .OrderByDescending(o => o.Id)
            .ToPaginatedResultAsync(pageNumber, pageSize);

        return paginatedOrders;
    }

    public async Task<bool> ExistsAsync(string number)
    {
        return await _context.Orders.AnyAsync(o => o.Number.Equals(number));
    }

    private IQueryable<Order> GetQueryableOrders()
    {
        return _context.Orders
            .Include(o => o.Status)
            .Include(o => o.PaymentMethod)
            .Include(o => o.ShippingMethod)
            .Include(o => o.WeightUnit)
            .Include(o => o.Items);
    }
}