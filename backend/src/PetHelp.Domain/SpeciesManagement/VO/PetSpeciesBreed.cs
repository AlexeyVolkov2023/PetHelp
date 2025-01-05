namespace PetHelp.Domain.SpeciesManagement.VO;

public record PetSpeciesBreed
{
    public PetSpeciesBreed(Guid speciesId, Guid breedId)
     {
         SpeciesId = speciesId;
         BreedId = breedId;
     }
    
    public Guid SpeciesId { get; }
    public Guid BreedId { get; }
    
}