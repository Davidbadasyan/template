namespace Orders.Application.Dtos;

public class OrderItemDto : IRequestDto, IResponseDto
{
    public long Id { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public byte Units { get; set; }
}