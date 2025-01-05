namespace PetHelp.Domain.AnimalManagement.VO;

public record SocialNetwork
{
    public SocialNetwork(string name, string link)
    {
        Name = name;
        Link = link;
    }

    public string Name { get; }
    public string Link { get; }
}