using CSharpFunctionalExtensions;

namespace PetHelp.Domain.Shared;

public class PaymentDetail : ComparableValueObject
{
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
}