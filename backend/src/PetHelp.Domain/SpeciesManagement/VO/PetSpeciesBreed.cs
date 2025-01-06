using CSharpFunctionalExtensions;

namespace PetHelp.Domain.SpeciesManagement.VO;

public class PetSpeciesBreed : ComparableValueObject
{
    public PetSpeciesBreed(Guid speciesId, Guid breedId)
     {
         SpeciesId = speciesId;
         BreedId = breedId;
     }
    
    public Guid SpeciesId { get; }
    public Guid BreedId { get; }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return SpeciesId;
        yield return BreedId;
    }
}