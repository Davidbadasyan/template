namespace Identity.API.Application.Services;

public class ProfileService : IProfileService
{
    private readonly AppUserManager _userManager;

    public ProfileService(AppUserManager userManager)
    {
        _userManager = userManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var user = await _userManager.GetUserAsync(context.Subject);
        var claims = await _userManager.GetClaimsAsync(user);

        context.IssuedClaims.AddRange(claims);
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var user = await _userManager.GetUserAsync(context.Subject);

        context.IsActive = user != null;
    }
}