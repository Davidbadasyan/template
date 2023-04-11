namespace Identity.API.Application.Services;

public class IdentityUserService : IIdentityUserService
{
    private readonly AppUserManager _userManager;
    public IdentityUserService(AppUserManager userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> RegisterAsync(UserRegister userRegister)
    {
        var user = await _userManager.FindByNameAsync(userRegister.Email);
        if (user != null)
        {
            return IdentityResult.Failed(new IdentityError() { Code = "UserExists", Description = "User already exists" });
        }

        IdentityResult result = null;
        user = new ApplicationUser { UserName = userRegister.Email, Email = userRegister.Email };
        try
        {
            result = await _userManager.CreateAsync(user, userRegister.Password);
            if (!result.Succeeded)
                throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));

            result = await _userManager.AddClaimsAsync(user, new List<Claim>()
            {
                //new Claim("userId", userRegister.Id.ToString()),
                new Claim("email", user.NormalizedEmail),
                new Claim("firstName", userRegister.FirstName),
                new Claim("lastName", userRegister.LastName),
                new Claim("status", userRegister.Status),
                new Claim("role", userRegister.Role),
            });

            if (!result.Succeeded)
                throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
        }
        catch (Exception ex)
        {
            result = IdentityResult.Failed(
                new IdentityError() { Code = "UserCreationFailed", Description = ex.Message });
            await _userManager.DeleteAsync(user);
        }

        return result;
    }
}