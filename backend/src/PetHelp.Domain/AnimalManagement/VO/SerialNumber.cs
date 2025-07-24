using CSharpFunctionalExtensions;
using PetHelp.Domain.Shared;

namespace PetHelp.Domain.AnimalManagement.VO;

public class SerialNumber : ComparableValueObject
{
    
    private SerialNumber(int value)
    {
        Value = value;
    }
    public int Value { get;  }

    public static Result<SerialNumber, Error> Create(int value)
    {
        if (value <= 0 || value > Constants.SERIAL_NUMBER_MAX_LENGTH)
            return Errors.General.ValueIsInvalid("serial number");

        return new SerialNumber(value);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}