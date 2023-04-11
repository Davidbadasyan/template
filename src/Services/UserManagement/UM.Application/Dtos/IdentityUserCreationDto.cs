namespace UM.Application.Dtos;

public record IdentityUserCreationDto
{
    public string Email { get; init; }
    public string Password { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Status { get; init; }
    public string Role { get; init; }
}