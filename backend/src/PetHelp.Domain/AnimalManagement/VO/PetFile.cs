using CSharpFunctionalExtensions;

namespace PetHelp.Domain.AnimalManagement.VO;

public class PetFile : ComparableValueObject
{
    public PetFile() {}
    public PetFile(FilePath pathToStorage)
    {
        PathToStorage = pathToStorage;
    }
   
    public FilePath PathToStorage { get; }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return PathToStorage;
    }
}