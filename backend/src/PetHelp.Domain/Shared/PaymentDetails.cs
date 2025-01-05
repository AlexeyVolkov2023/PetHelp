namespace PetHelp.Domain.Shared;

public record PaymentDetails
{
    public PaymentDetails(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public string Title { get; }
    public string Description { get; }
}
        
   