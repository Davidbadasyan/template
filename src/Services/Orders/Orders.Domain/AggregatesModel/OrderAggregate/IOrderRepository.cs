namespace Orders.Domain.AggregatesModel.OrderAggregate;

public interface IOrderRepository : IRepository<Order>
{
    Task<Order> AddAsync(Order user);
    Task<Order?> GetByIdAsync(long id, string buyer);
}