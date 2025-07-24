using CSharpFunctionalExtensions;
using PetHelp.Domain.Shared;
using PetHelp.Domain.SpeciesManagement.ID;

namespace PetHelp.Domain.SpeciesManagement.Entities;

public class Breed : Entity<BreedId>
{
    private Breed(
        BreedId id,
        string name) : base(id)
    {
        Name = name;
    }
    public string Name { get; }
    
    public static Result<Breed, Error> Create(
        BreedId id,
        string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsInvalid("name");
        
        return new Breed(id, name);
    }
}