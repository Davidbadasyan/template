namespace Orders.Infrastructure.Repositories.Queries;

public class LookupQueryRepository : ILookupQueryRepository
{
    private readonly OrdersQueryContext _context;
    public LookupQueryRepository(OrdersQueryContext context)
    {
        _context = context;
    }

    public IQueryable<PaymentMethod> PaymentMethods => _context.PaymentMethods;
    public IQueryable<ShippingMethod> ShippingMethods => _context.ShippingMethods;
    public IQueryable<WeightUnit> WeightUnits => _context.WeightUnits;
}
