namespace Orders.Domain.AggregatesModel.OrderAggregate;

public interface IOrderQueryRepository
{
    Task<Order?> GetByIdAsync(long id, string buyer);

    Task<PaginatedResult<Order>> GetPaginatedAsync(
        string buyer,
        int pageNumber,
        int pageSize);
    Task<bool> ExistsAsync(string number);
}