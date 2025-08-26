using CSharpFunctionalExtensions;
using PetHelp.Domain.Shared;

namespace PetHelp.Domain.AnimalManagement.VO;

public class Position : ComparableValueObject
{
    public static Position First = new(1);

    private Position(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public Result<Position, Error> Forward()
        => Create(Value + 1);

    public Result<Position, Error> Back()
        => Create(Value - 1);

    public static Result<Position, Error> Create(int value)
    {
        if (value <= 0 || value > Constants.SERIAL_NUMBER_MAX_LENGTH)
            return Errors.General.ValueIsInvalid("position");

        return new Position(value);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}