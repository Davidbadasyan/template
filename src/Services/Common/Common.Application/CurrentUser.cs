namespace Common.Application;

public interface ICurrentUser
{
    string FirstName { get; }
    string LastName { get; }
    string Email { get; }
}

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public string FirstName => _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value ?? string.Empty;
    public string LastName => _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value ?? string.Empty;
    public string Email => _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? string.Empty;
}
