namespace Orders.Domain.AggregatesModel.OrderAggregate;

public class OrderItem : Entity
{
    // DDD Patterns comment
    // Using private fields, allowed since EF Core 1.1, is a much better encapsulation
    // aligned with DDD Aggregates and Domain Entities (Instead of properties and property collections)

    public Order Order { get; private set; }
    private long _orderId;

    public string ProductName { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Discount { get; private set; }
    public byte Units { get; private set; }

    public OrderItem()
    {
    }

    public OrderItem(
        long id,
        string productName,
        decimal unitPrice,
        decimal discount,
        byte units = 1)
    {
        if (units <= 0)
        {
            throw new OrderDomainException("Invalid number of units");
        }

        if ((unitPrice * units) < discount)
        {
            throw new OrderDomainException("The total of order item is lower than applied discount");
        }

        Id = id;
        ProductName = productName;
        UnitPrice = unitPrice;
        Discount = discount;
        Units = units;
    }

    public void Update(
        string productName,
        decimal unitPrice,
        decimal discount,
        byte units = 1)
    {
        ProductName = productName;
        UnitPrice = unitPrice;
        Discount = discount;
        Units = units;
    }


    public void SetNewDiscount(decimal discount)
    {
        if (discount < 0)
        {
            throw new OrderDomainException("Discount is not valid");
        }

        Discount = discount;
    }

    public void AddUnits(byte units)
    {
        if (units < 0)
        {
            throw new OrderDomainException("Invalid units");
        }

        Units += units;
    }
}
