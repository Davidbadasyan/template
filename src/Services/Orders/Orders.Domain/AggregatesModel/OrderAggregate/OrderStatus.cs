namespace Orders.Domain.AggregatesModel.OrderAggregate;

public class OrderStatus : Enumeration
{
    public static OrderStatus Submitted = new(1, "Submitted");
    public static OrderStatus AwaitingValidation = new(2, "Awaiting validation");
    public static OrderStatus StockConfirmed = new(3, "Stock confirmed");
    public static OrderStatus Paid = new(4, "Paid");
    public static OrderStatus Shipped = new(5, "Shipped");
    public static OrderStatus Cancelled = new(6, "Cancelled");

    public OrderStatus(int id, string name)
        : base(id, name)
    {
    }

    public static IEnumerable<OrderStatus> List() =>
        new[] { Submitted, AwaitingValidation, StockConfirmed, Paid, Shipped, Cancelled };

    public static OrderStatus FromName(string name)
    {
        var state = List()
            .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

        if (state == null)
        {
            throw new OrderDomainException($"Possible values for Status: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }

    public static OrderStatus From(int id)
    {
        var state = List().SingleOrDefault(s => s.Id == id);

        if (state == null)
        {
            throw new OrderDomainException($"Possible values for Status: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }
}