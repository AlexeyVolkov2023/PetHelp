using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PetHelp.Domain.Shared;

namespace PetHelp.Domain.AnimalManagement.VO;

public class PhoneNumber : ComparableValueObject
{
    private PhoneNumber(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<PhoneNumber, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, Constants.PHONE_NUMBER_REGEX))
            return Errors.General.ValueIsInvalid("phone number");

        return new PhoneNumber(value);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}