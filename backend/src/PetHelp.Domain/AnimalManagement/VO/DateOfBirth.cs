using CSharpFunctionalExtensions;
using PetHelp.Domain.Shared;

namespace PetHelp.Domain.AnimalManagement.VO;

public class DateOfBirth : ComparableValueObject
{
    private DateOfBirth(DateTime date)
    {
        Date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
    }

    public DateTime Date { get; }

    public static Result<DateOfBirth, Error> Create(DateTime date)
    {
        date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
        
        if (date > DateTime.UtcNow || date < new DateTime(1900, 1, 1))
            return Errors.General.ValueIsRequired("DateOfBirth");

        return new DateOfBirth(date);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Date;
    }
}