using CSharpFunctionalExtensions;
using PetHelp.Domain.Shared;

namespace PetHelp.Domain.AnimalManagement.VO;

public class FullName : ComparableValueObject
{
    private FullName(string name, string surname, string patronymic)
    {
        Name = name;
        Surname = surname;
        Patronymic = patronymic;
    }

    public string Name { get; }
    public string Surname { get; }
    public string? Patronymic { get; }

    public static Result<FullName, Error> Create(
        string name,
        string surname,
        string patronymic)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > Constants.NAME_MAX_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Name");
        }

        if (string.IsNullOrWhiteSpace(surname) || surname.Length > Constants.SURNAME_MAX_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Surname");
        }

        if (string.IsNullOrWhiteSpace(patronymic) || patronymic.Length > Constants.PATRONYMIC_MAX_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Patronymic"); }


        return new FullName(name, surname, patronymic);
    }


    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Name;
        yield return Surname;
        if (Patronymic != null)
        {
            yield return Patronymic;
        }
    }
}