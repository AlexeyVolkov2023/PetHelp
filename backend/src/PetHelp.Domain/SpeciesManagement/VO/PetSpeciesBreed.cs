using CSharpFunctionalExtensions;
using PetHelp.Domain.Shared;
using PetHelp.Domain.SpeciesManagement.ID;

namespace PetHelp.Domain.SpeciesManagement.VO;

public class PetSpeciesBreed : ComparableValueObject
{
    private PetSpeciesBreed(Guid speciesId, Guid breedId)
     {
         SpeciesId = speciesId;
         BreedId = breedId;
     }
    
    public Guid SpeciesId { get; }
    public Guid BreedId { get; }
    
    public static Result<PetSpeciesBreed, Error> Create(Guid speciesId, Guid breedId)
    {
        return new PetSpeciesBreed(speciesId, breedId);
        
    }
    
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
         {
             yield return SpeciesId;
             yield return BreedId;
         }
}

