namespace Orders.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrdersContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public OrderRepository(OrdersContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Order> AddAsync(Order user)
    {
        return (await _context.Orders.AddAsync(user)).Entity;
    }

    public async Task<Order?> GetByIdAsync(long id, string buyer)
    {
        return await _context
            .Orders
            .Include(o => o.Status)
            .Include(o => o.PaymentMethod)
            .Include(o => o.ShippingMethod)
            .Include(o => o.WeightUnit)
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id &&
                                      o.Buyer.Equals(buyer));
    }
}