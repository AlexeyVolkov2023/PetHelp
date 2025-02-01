using CSharpFunctionalExtensions;
using PetHelp.Domain.Shared;

namespace PetHelp.Domain.AnimalManagement.VO;

public class PetStatus : ComparableValueObject
{
    private static readonly PetStatus NeedsHelp = new(nameof(NeedsHelp));
    private static readonly PetStatus LookingForHome = new(nameof(LookingForHome));
    private static readonly PetStatus FoundHome = new(nameof(FoundHome));

    private static readonly PetStatus[] _all = [NeedsHelp, LookingForHome, FoundHome];


    private PetStatus(string value)
    {
        Value = value;
    }

    public string? Value { get; }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }

    public static Result<PetStatus, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Errors.General.ValueIsInvalid("Value");
        }

        var petStatus = new PetStatus(value);

        return new PetStatus(value);
    }
}