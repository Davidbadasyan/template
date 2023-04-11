namespace Orders.Application.Queries;

public interface ILookupQueryService
{
    public List<IdNameResult> PaymentMethods { get; }
    public List<IdNameResult> ShippingMethods { get; }
    public List<IdNameResult> WeightUnits { get; }
}