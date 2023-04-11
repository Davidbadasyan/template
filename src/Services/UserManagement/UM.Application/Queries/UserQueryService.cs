namespace UM.Application.Queries;

public class UserQueryService : BaseQuery, IUserQueryService
{
    private readonly IUserQueryRepository _userQueryRepository;

    public UserQueryService(
        IMapper mapper,
        ICurrentUser currentUser,
        IUserQueryRepository userQueryRepository) : base(mapper, currentUser)
    {
        _userQueryRepository = userQueryRepository;
    }

    public async Task<UserDto> GetMeAsync()
    {
        var user = await _userQueryRepository.GetByEmailAsync(CurrentUser.Email);

        return Mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetByIdAsync(long id)
    {
        var user = await _userQueryRepository.GetByIdAsync(id);

        return Mapper.Map<UserDto>(user);
    }
}