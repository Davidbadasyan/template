namespace Orders.Application.Dtos;

public class OrderRequestDto : OrderDto, IRequestDto
{
    public int PaymentMethodId { get; set; }
    public int ShippingMethodId { get; set; }
    public int WeightUnitId { get; set; }
}