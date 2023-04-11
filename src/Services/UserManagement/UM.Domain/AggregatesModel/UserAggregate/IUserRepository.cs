namespace UM.Domain.AggregatesModel.UserAggregate;

public interface IUserRepository : IRepository<User>
{
    Task<User> AddAsync(User user);
    Task<User?> GetByIdAsync(long id);
}