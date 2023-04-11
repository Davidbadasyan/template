namespace UM.Application.Dtos;

public record UserDto : IResponseDto
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ApprovalStatus { get; set; }
    public string Role { get; set; }
}