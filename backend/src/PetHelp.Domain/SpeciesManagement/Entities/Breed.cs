using CSharpFunctionalExtensions;
using PetHelp.Domain.Shared;
using PetHelp.Domain.SpeciesManagement.AggregateRoot;
using PetHelp.Domain.SpeciesManagement.ID;

namespace PetHelp.Domain.SpeciesManagement.Entities;

public class Breed : Entity<BreedId>
{
    public Species Species { get; private set; } = null!;
    private Breed(
        BreedId id,
        
        string title) : base(id)
    {
        
        Title = title;
    }
    public string Title { get; }
    
    public static Result<Breed, Error> Create(
        BreedId id,
        string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return Errors.General.ValueIsInvalid("title");
        
        return new Breed(id, title);
    }
}