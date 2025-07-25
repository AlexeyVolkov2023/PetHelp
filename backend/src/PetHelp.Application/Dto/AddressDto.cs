namespace PetHelp.Application.Dto;

public record AddressDto(
    string Street,
    string HouseNumber,
    int? Apartment,
    string City,
    string Region,
    string Country);