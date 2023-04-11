namespace UM.Infrastructure.Services.External;

public class IdentityService : IIdentityService
{
    private readonly HttpClientHelper _httpClientHelper;
    public IdentityService(
        IHttpClientFactory httpClientFactory,
        string baseAddress)
    {
        _httpClientHelper = new HttpClientHelper(httpClientFactory, baseAddress);
    }

    public async Task<string> CreateUserAsync(IdentityUserCreationDto identityUserCreation)
    {
        var message = await _httpClientHelper.PostAsync("api/account/register", identityUserCreation);

        return message;
    }
}