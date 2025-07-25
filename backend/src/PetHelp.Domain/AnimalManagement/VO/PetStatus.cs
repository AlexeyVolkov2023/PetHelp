using CSharpFunctionalExtensions;
using PetHelp.Domain.Shared;

namespace PetHelp.Domain.AnimalManagement.VO;

public class PetStatus : ComparableValueObject
{
    private static readonly PetStatus NeedsHelp = new(nameof(NeedsHelp));
    private static readonly PetStatus LookingForHome = new(nameof(LookingForHome));
    private static readonly PetStatus FoundHome = new(nameof(FoundHome));

    private static readonly PetStatus[] _all = [NeedsHelp, LookingForHome, FoundHome];


    private PetStatus(string status)
    {
        Status = status;
    }

    public string Status { get; }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Status;
    }

    public static Result<PetStatus, Error> Create(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            return Errors.General.ValueIsInvalid("pet status");

        var petStatus = new PetStatus(status);

        return new PetStatus(status);
    }
}