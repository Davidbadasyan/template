namespace UM.Application.Dtos;

public record AddressDto(
    string Street,
    string City,
    string State,
    string Country,
    string ZipCode);