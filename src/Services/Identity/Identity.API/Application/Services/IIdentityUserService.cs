namespace Identity.API.Application.Services;

public interface IIdentityUserService
{
    Task<IdentityResult> RegisterAsync(UserRegister userRegister);
}