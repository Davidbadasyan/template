namespace Orders.Domain.AggregatesModel.OrderAggregate;

public class PaymentMethod : Enumeration
{
    public static PaymentMethod CashOnDelivery = new(1, "Cash on delivery");
    public static PaymentMethod DebitCard = new(2, "Debit card");
    public static PaymentMethod CreditCard = new(3, "Credit card");

    public PaymentMethod(int id, string name)
        : base(id, name)
    {
    }

    public static IEnumerable<PaymentMethod> List() =>
        new[] { CashOnDelivery, DebitCard, CreditCard, };

    public static PaymentMethod FromName(string name)
    {
        var state = List()
            .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

        if (state == null)
        {
            throw new OrderDomainException($"Possible values for Payment Method: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }

    public static PaymentMethod From(int id)
    {
        var state = List().SingleOrDefault(s => s.Id == id);

        if (state == null)
        {
            throw new OrderDomainException($"Possible values for Payment Method: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }
}