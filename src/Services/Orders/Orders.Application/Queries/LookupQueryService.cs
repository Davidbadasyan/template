namespace Orders.Application.Queries;
public class LookupQueryService : ILookupQueryService
{
    private readonly ILookupQueryRepository _lookupQueryRepository;

    private readonly Lazy<List<IdNameResult>> _paymentMethods;
    private readonly Lazy<List<IdNameResult>> _shippingMethods;
    private readonly Lazy<List<IdNameResult>> _weightUnits;
    public LookupQueryService(ILookupQueryRepository lookupQueryRepository)
    {
        _lookupQueryRepository = lookupQueryRepository;

        _paymentMethods = new Lazy<List<IdNameResult>>(() => _lookupQueryRepository.PaymentMethods.Select(x => new IdNameResult(x.Id, x.Name)).ToList());
        _shippingMethods = new Lazy<List<IdNameResult>>(() => _lookupQueryRepository.ShippingMethods.Select(x => new IdNameResult(x.Id, x.Name)).ToList());
        _weightUnits = new Lazy<List<IdNameResult>>(() => _lookupQueryRepository.WeightUnits.Select(x => new IdNameResult(x.Id, x.Name)).ToList());
    }

    public List<IdNameResult> PaymentMethods => _paymentMethods.Value;
    public List<IdNameResult> ShippingMethods => _shippingMethods.Value;
    public List<IdNameResult> WeightUnits => _weightUnits.Value;
}