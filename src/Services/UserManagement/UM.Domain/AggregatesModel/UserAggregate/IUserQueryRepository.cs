namespace UM.Domain.AggregatesModel.UserAggregate;

public interface IUserQueryRepository
{
    Task<User?> GetByIdAsync(long id);
    Task<User?> GetByEmailAsync(string email);
    Task<bool> UserExistsAsync(string email);
    Task<bool> UserExistsAsync(long id);
}