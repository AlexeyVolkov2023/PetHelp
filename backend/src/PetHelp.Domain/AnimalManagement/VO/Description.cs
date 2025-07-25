using CSharpFunctionalExtensions;
using PetHelp.Domain.Shared;

namespace PetHelp.Domain.AnimalManagement.VO;

public class Description : ComparableValueObject
{
    private Description(string value)
    {
        Value = value;
    }
    public string Value { get; }

    public static Result<Description, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > Constants.DESCRIPTION_MAX_LENGTH)
            return Errors.General.ValueIsInvalid("description");

        return new Description(value);
    }
   
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}