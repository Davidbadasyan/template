namespace Orders.Domain.AggregatesModel.OrderAggregate;

public class ShippingMethod : Enumeration
{
    public static ShippingMethod Fedex = new(1, "Fedex");
    public static ShippingMethod Ups = new(2, "UPS");
    public static ShippingMethod Usps = new(3, "USPS");

    public ShippingMethod(int id, string name)
        : base(id, name)
    {
    }

    public static IEnumerable<ShippingMethod> List() =>
        new[] { Fedex, Ups, Usps, };

    public static ShippingMethod FromName(string name)
    {
        var state = List()
            .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

        if (state == null)
        {
            throw new OrderDomainException($"Possible values for Shipping Method: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }

    public static ShippingMethod From(int id)
    {
        var state = List().SingleOrDefault(s => s.Id == id);

        if (state == null)
        {
            throw new OrderDomainException($"Possible values for Shipping Method: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }
}