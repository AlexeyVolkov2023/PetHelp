using CSharpFunctionalExtensions;
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
}