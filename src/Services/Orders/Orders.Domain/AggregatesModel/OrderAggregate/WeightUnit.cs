namespace Orders.Domain.AggregatesModel.OrderAggregate;

public class WeightUnit : Enumeration
{
    public static WeightUnit Gram = new(1, "g");
    public static WeightUnit Kilogram = new(2, "kg");
    public static WeightUnit Tonne = new(3, "t");

    public WeightUnit(int id, string name)
        : base(id, name)
    {
    }

    public static IEnumerable<WeightUnit> List() =>
        new[] { Gram, Kilogram, Tonne };

    public static WeightUnit FromName(string name)
    {
        var state = List()
            .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

        if (state == null)
        {
            throw new OrderDomainException($"Possible values for Weight Unit: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }

    public static WeightUnit From(int id)
    {
        var state = List().SingleOrDefault(s => s.Id == id);

        if (state == null)
        {
            throw new OrderDomainException($"Possible values for Weight Unit: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }
}