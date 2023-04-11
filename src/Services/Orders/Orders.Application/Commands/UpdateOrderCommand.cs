namespace Orders.Application.Commands;

public class UpdateOrderCommand : BaseCommand<OrderResponseDto>
{
    public long OrderId { get; set; }
    public OrderRequestDto OrderRequest { get; set; }

    public UpdateOrderCommand(
        long orderId,
        OrderRequestDto orderRequest)
    {
        OrderId = orderId;
        OrderRequest = orderRequest;
    }
    public class CreateOrderCommandHandler : BaseCommandHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(
            IMapper mapper,
            ICurrentUser currentUser,
            IOrderRepository orderRepository) : base(mapper, currentUser)
        {
            _orderRepository = orderRepository;
        }

        public override async Task<ResponseModel<OrderResponseDto>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, CurrentUser.Email);
            if (order is null)
            {
                return ResponseModel<OrderResponseDto>.Create(ResponseType.Warning, message: "Order does not exist.");
            }

            order.Update(
                CurrentUser.Email,
                request.OrderRequest.Items
                    .Select(oi =>
                        new OrderItem(
                            oi.Id,
                            oi.ProductName,
                            oi.UnitPrice,
                            oi.Discount,
                            oi.Units)).ToList(),
                new Address(
                    request.OrderRequest.Address?.Street,
                    request.OrderRequest.Address?.City,
                    request.OrderRequest.Address?.State,
                    request.OrderRequest.Address?.Country,
                    request.OrderRequest.Address?.ZipCode),
                PaymentMethod.From(request.OrderRequest.PaymentMethodId),
                ShippingMethod.From(request.OrderRequest.ShippingMethodId),
                request.OrderRequest.Weight,
                WeightUnit.From(request.OrderRequest.WeightUnitId),
                request.OrderRequest.Description);


            await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return ResponseModel<OrderResponseDto>.Create(ResponseType.Success, Mapper.Map<OrderResponseDto>(order));
        }
    }
}