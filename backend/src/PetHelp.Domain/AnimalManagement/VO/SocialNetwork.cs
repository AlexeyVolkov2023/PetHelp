using CSharpFunctionalExtensions;

namespace PetHelp.Domain.AnimalManagement.VO;

public class SocialNetwork : ComparableValueObject
{
    public SocialNetwork(string name, string link)
    {
        Name = name;
        Link = link;
    }

    public string Name { get; }
    public string Link { get; }
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Name;
        yield return Link;
    }
}