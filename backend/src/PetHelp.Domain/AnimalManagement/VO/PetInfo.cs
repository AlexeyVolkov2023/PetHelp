using CSharpFunctionalExtensions;
using PetHelp.Domain.Shared;

namespace PetHelp.Domain.AnimalManagement.VO;

public class PetInfo : ComparableValueObject
{
    private PetInfo(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; }
    public string Description { get; }


    public static Result<PetInfo, Error> Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > Constants.NAME_MAX_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Name");
        }

        if (string.IsNullOrWhiteSpace(description) || description.Length > Constants.DESCRIPTION_MAX_LENGTH)
        {
            return Errors.General.ValueIsInvalid($"Description");
        }

        return new PetInfo(name, description);

    }
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Name;
        yield return Description;
    }
}