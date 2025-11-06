namespace PetHelp.Application.Dto;

public class AddressDto
{
    public string Street { get; init; } = String.Empty;
    public string HouseNumber { get; init; } = String.Empty;
    public int? Apartment { get; init; } 
    public string City { get; init; } = String.Empty;
    public string Region { get; init; } = String.Empty;
    public string Country { get; init; } = String.Empty;
}