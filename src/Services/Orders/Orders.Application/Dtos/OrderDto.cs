namespace Orders.Application.Dtos;

public class OrderDto
{
    public long Id { get; set; }
    public string Number { get; set; }
    public string? Description { get; set; }
    public int Weight { get; set; }
    public bool IsDraft { get; set; }
    public int PaymentMethodId { get; set; }
    public int ShippingMethodId { get; set; }
    public int WeightUnitId { get; set; }
    public AddressDto? Address { get; set; }
    public List<OrderItemDto> Items { get; set; }
}