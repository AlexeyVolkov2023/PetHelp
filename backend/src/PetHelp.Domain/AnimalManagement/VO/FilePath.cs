using CSharpFunctionalExtensions;
using PetHelp.Domain.Shared;

namespace PetHelp.Domain.AnimalManagement.VO;

public class FilePath : ComparableValueObject
{
    private FilePath(string path)
    {
        Path = path;
    }

    public string Path { get; }

    public static Result<FilePath, Error> Create(Guid path, string extensions)
    {
        if (string.IsNullOrWhiteSpace(path.ToString()))
            return Errors.General.ValueIsInvalid("path"); 
        
        if (string.IsNullOrWhiteSpace(extensions))
            return Errors.General.ValueIsInvalid("extensions");

        var fullPath = path + "." + extensions;

        return new FilePath(fullPath);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Path;
    }
}