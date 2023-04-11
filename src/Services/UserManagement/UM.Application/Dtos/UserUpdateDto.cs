namespace UM.Application.Dtos;

public record UserUpdateDto(string FirstName, string LastName) : IRequestDto;