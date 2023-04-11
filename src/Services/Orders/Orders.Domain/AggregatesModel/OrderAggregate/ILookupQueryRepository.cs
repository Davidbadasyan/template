namespace Orders.Domain.AggregatesModel.OrderAggregate;

public interface ILookupQueryRepository
{
    public IQueryable<PaymentMethod> PaymentMethods { get; }
    public IQueryable<ShippingMethod> ShippingMethods { get; }
    public IQueryable<WeightUnit> WeightUnits { get; }
}