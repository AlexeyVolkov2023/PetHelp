using CSharpFunctionalExtensions;

namespace PetHelp.Domain.Shared;

public class PaymentDetail : ComparableValueObject
{
    public PaymentDetail()
    {
        
    }
    private PaymentDetail(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public string Title { get; } 
    public string Description { get; } 

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Title;
        yield return Description;
    }
    
    public static Result<PaymentDetail, Error> Create(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Errors.General.ValueIsInvalid("Title");
        } 
        
        if (string.IsNullOrWhiteSpace(description))
        {
            return Errors.General.ValueIsInvalid("Description");
        }

        return new PaymentDetail(title, description);
    }
}