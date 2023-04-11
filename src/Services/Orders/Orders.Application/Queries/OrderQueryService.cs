namespace Orders.Application.Queries;

public class OrderQueryService : BaseQuery, IOrderQueryService
{
    private readonly IOrderQueryRepository _orderQueryRepository;

    public OrderQueryService(
        IMapper mapper,
        ICurrentUser currentUser,
        IOrderQueryRepository orderQueryRepository) : base(mapper, currentUser)
    {
        _orderQueryRepository = orderQueryRepository;
    }

    public async Task<OrderResponseDto?> GetByIdAsync(long id)
    {
        var order = await _orderQueryRepository.GetByIdAsync(id, CurrentUser.Email);
        if (order is null)
            return null;
        var mappedOrder = Mapper.Map<OrderResponseDto>(order);

        //TODO : Get from UmService
        mappedOrder.Buyer = Mapper.Map<BuyerDto>(CurrentUser);

        return mappedOrder;
    }

    public async Task<PaginatedResult<OrderResponseDto>> GetPaginatedAsync(int pageNumber, int pageSize)
    {
        var paginatedOrders = await _orderQueryRepository.GetPaginatedAsync(CurrentUser.Email, pageNumber, pageSize);

        var mappedPaginatedOrders = Mapper.Map<PaginatedResult<OrderResponseDto>>(paginatedOrders);

        foreach (var orderResponseDto in mappedPaginatedOrders.Data)
        {
            //TODO : Get from UmService
            orderResponseDto.Buyer = Mapper.Map<BuyerDto>(CurrentUser);
        }

        return mappedPaginatedOrders;
    }
}