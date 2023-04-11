namespace Orders.Application.Queries;

public interface IOrderQueryService
{
    Task<OrderResponseDto?> GetByIdAsync(long id);
    Task<PaginatedResult<OrderResponseDto>> GetPaginatedAsync(int pageNumber, int pageSize);
}