namespace Orders.Application.Dtos;

public class OrderResponseDto : OrderDto, IResponseDto
{
    public BuyerDto Buyer { get; set; }
    public string Status { get; set; }
    public string PaymentMethod { get; set; }
    public string ShippingMethod { get; set; }
    public string WeightUnit { get; set; }
}