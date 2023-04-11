namespace UM.Application.Queries;

public interface IUserQueryService
{
    Task<UserDto> GetMeAsync();
    Task<UserDto> GetByIdAsync(long id);
}
