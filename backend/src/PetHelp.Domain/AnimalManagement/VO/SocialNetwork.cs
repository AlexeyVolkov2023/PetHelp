using CSharpFunctionalExtensions;
using PetHelp.Domain.Shared;

namespace PetHelp.Domain.AnimalManagement.VO;

public class SocialNetwork : ComparableValueObject
{
    public SocialNetwork()
    {
    }
    private SocialNetwork(string name, string link)
    {
        Name = name;
        Link = link;
    }

    public string Name { get; } 
    public string Link { get; } 
    
    public static Result<SocialNetwork, Error> Create(string name, string link)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > Constants.SOCIAL_NAME_MAX_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Name");
        }

        if (string.IsNullOrWhiteSpace(link) || link.Length > Constants.SOCIAL_LINK_MAX_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Link");
        }

        return new SocialNetwork(name, link);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Name;
        yield return Link;
    }

    
}