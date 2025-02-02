using CSharpFunctionalExtensions;
using PetHelp.Domain.Shared;

namespace PetHelp.Domain.AnimalManagement.VO;

public class Address : ComparableValueObject
{
    private Address(
        string country,
        string region,
        string city,
        string street,
        string houseNumber,
        int? apartment)
    {
        Country = country;
        Region = region;
        City = city;
        Street = street;
        HouseNumber = houseNumber;
        Apartment = apartment;
    }

    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public int? Apartment { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }


    public static Result<Address, Error> Create(
        string country,
        string region,
        string city,
        string street,
        string houseNumber,
        int? apartment)
    {
        if (string.IsNullOrWhiteSpace(country) || country.Length > Constants.ADDRESS_MAX_LENGTH)
        {
            return Errors.General.ValueIsInvalid("country");
        }

        if (string.IsNullOrWhiteSpace(region) || region.Length > Constants.ADDRESS_MAX_LENGTH)
        {
            return Errors.General.ValueIsInvalid("region");
        }

        if (string.IsNullOrWhiteSpace(city) || city.Length > Constants.ADDRESS_MAX_LENGTH)
        {
            return Errors.General.ValueIsInvalid("city");
        }

        if (string.IsNullOrWhiteSpace(street) || street.Length > Constants.ADDRESS_MAX_LENGTH)
        {
            return Errors.General.ValueIsInvalid("street");
        }

        if (string.IsNullOrWhiteSpace(houseNumber) || houseNumber.Length > Constants.HOUS_NUMBER_MAX_VALUE)
        {
            return Errors.General.ValueIsInvalid("house_number");
        }

        if (apartment <= 0 || apartment > Constants.APPARTMENT_MAX_VALUE)
        {
            return Errors.General.ValueIsInvalid("apartment");
        }

        return new Address(
            country,
            region,
            city,
            street,
            houseNumber,
            apartment);
    }


    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
       yield return Country ;
       yield return Region ;
       yield return City ;
       yield return Street ;
       yield return HouseNumber ;
       yield return Apartment! ;
    }
}