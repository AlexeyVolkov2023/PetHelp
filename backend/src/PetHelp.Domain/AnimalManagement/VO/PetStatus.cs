namespace PetHelp.Domain.AnimalManagement.VO;

public record PetStatus
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
    
}