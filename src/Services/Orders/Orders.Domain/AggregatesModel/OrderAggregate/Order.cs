namespace Orders.Domain.AggregatesModel.OrderAggregate;

public class Order : Entity, IAggregateRoot
{
    public Address Address { get; private set; }

    public string Number { get; private set; }

    public string Buyer { get; private set; }

    public OrderStatus Status { get; private set; }
    private int _statusId;

    public PaymentMethod PaymentMethod { get; private set; }
    private int _paymentMethodId;

    public ShippingMethod ShippingMethod { get; private set; }
    private int _shippingMethodId;

    public string Description { get; private set; }

    public int Weight { get; private set; }
    public WeightUnit WeightUnit { get; private set; }
    private int _weightUnitId;

    public bool IsDraft { get; private set; }

    private List<OrderItem> _items;
    public IReadOnlyCollection<OrderItem> Items => _items;

    protected Order()
    {
        _items = new List<OrderItem>();
        IsDraft = false;
    }

    protected Order(
        string number,
        string buyer,
        List<OrderItem> orderItems,
        Address address,
        PaymentMethod paymentMethod,
        ShippingMethod shippingMethod,
        int weight,
        WeightUnit weightUnit,
        string description) : this()
    {
        _items = orderItems;
        Number = number;
        Buyer = buyer;
        _statusId = OrderStatus.Submitted.Id;
        _paymentMethodId = paymentMethod.Id;
        _shippingMethodId = shippingMethod.Id;
        Weight = weight;
        _weightUnitId = weightUnit.Id;
        Description = description;
        Address = address;
    }

    public static async Task<Order> CreateAsync(
        IOrderQueryRepository orderQueryRepository,
        string number,
        string buyer,
        List<OrderItem> orderItems,
        Address address,
        PaymentMethod paymentMethod,
        ShippingMethod shippingMethod,
        int weight,
        WeightUnit weightUnit,
        string description)
    {
        if (string.IsNullOrWhiteSpace(buyer))
            throw new OrderDomainException("Buyer is required.");

        var existsWithNumber = await orderQueryRepository.ExistsAsync(number);
        if (existsWithNumber)
            throw new OrderDomainException($"An order with number {number} already exists.");

        var order = new Order(number, buyer, orderItems, address, paymentMethod, shippingMethod, weight, weightUnit, description);

        return order;
    }

    public void Update(
        string buyer,
        List<OrderItem> orderItems,
        Address address,
        PaymentMethod paymentMethod,
        ShippingMethod shippingMethod,
        int weight,
        WeightUnit weightUnit,
        string description)
    {
        if (!Buyer.Equals(buyer))
            throw new OrderDomainException("Cannot update the order.");

        UpdateOrderItems(orderItems);
        Address = address;
        _paymentMethodId = paymentMethod.Id;
        _shippingMethodId = shippingMethod.Id;
        Weight = weight;
        _weightUnitId = weightUnit.Id;
        Description = description;
    }

    public void AddOrUpdateOrderItem(long id, string productName, decimal unitPrice, decimal discount, byte units = 1)
    {
        if (id == default)
        {
            _items.Add(new OrderItem(default, productName, unitPrice, discount, units));
        }
        else
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            if (item is null)
                throw new OrderDomainException("Order item not found.");

            item.Update(productName, unitPrice, discount, units);
        }
    }

    private void UpdateOrderItems(List<OrderItem> orderItems)
    {
        var itemsToBeRemoved = _items
            .Where(oi => orderItems.All(x => x.Id != oi.Id))
            .Select(oi => oi.Id)
            .ToList();

        _items.RemoveAll(oi => itemsToBeRemoved.Contains(oi.Id));
        orderItems.ForEach(oi => AddOrUpdateOrderItem(oi.Id, oi.ProductName, oi.UnitPrice, oi.Discount, oi.Units));
    }

    private void AddOrderStartedDomainEvent(string userId, string userName, int cardTypeId, string cardNumber,
            string cardSecurityNumber, string cardHolderName, DateTime cardExpiration)
    {
        var orderStartedDomainEvent = new OrderStartedDomainEvent(this, userId, userName, cardTypeId,
                                                                    cardNumber, cardSecurityNumber,
                                                                    cardHolderName, cardExpiration);

        AddDomainEvent(orderStartedDomainEvent);
    }

    private void StatusChangeException(OrderStatus orderStatusToChange)
    {
        throw new OrderDomainException($"Is not possible to change the order status from {Status.Name} to {orderStatusToChange.Name}.");
    }
}