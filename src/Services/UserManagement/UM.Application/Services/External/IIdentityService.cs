namespace UM.Application.Services.External;

public interface IIdentityService
{
    Task<string> CreateUserAsync(IdentityUserCreationDto createIdentityUserDto);
}