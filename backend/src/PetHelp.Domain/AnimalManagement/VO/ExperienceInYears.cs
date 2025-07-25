using CSharpFunctionalExtensions;
using PetHelp.Domain.Shared;

namespace PetHelp.Domain.AnimalManagement.VO;

public class ExperienceInYears : ComparableValueObject
{
    private ExperienceInYears(int value)
    {
        Value = value;
    }
    public int Value { get; }

    public static Result<ExperienceInYears, Error> Create(int value)
    {
        if (value is > Constants.EXPERIENCE_YEARS_MAX_VALUE or < 0)
            return Errors.General.ValueIsInvalid("experience in years");

        return new ExperienceInYears(value);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}