namespace Orders.Application.Commands;

public class CreateOrderCommand : BaseCommand<OrderResponseDto>
{
    public OrderRequestDto OrderRequest { get; set; }

    public CreateOrderCommand(OrderRequestDto orderRequest)
    {
        OrderRequest = orderRequest;
    }
    public class CreateOrderCommandHandler : BaseCommandHandler<CreateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderQueryRepository _orderQueryRepository;
        public CreateOrderCommandHandler(
            IMapper mapper,
            ICurrentUser currentUser,
            IOrderRepository orderRepository,
            IOrderQueryRepository orderQueryRepository) : base(mapper, currentUser)
        {
            _orderRepository = orderRepository;
            _orderQueryRepository = orderQueryRepository;
        }

        public override async Task<ResponseModel<OrderResponseDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await Order.CreateAsync(
                _orderQueryRepository,
                request.OrderRequest.Number,
                CurrentUser.Email,
                request.OrderRequest.Items
                    .Select(oi =>
                        new OrderItem(
                            default,
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

            await _orderRepository.AddAsync(order);
            await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return ResponseModel<OrderResponseDto>.Create(ResponseType.Success, Mapper.Map<OrderResponseDto>(order));
        }
    }
}