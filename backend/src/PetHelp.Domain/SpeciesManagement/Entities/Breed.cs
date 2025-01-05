using CSharpFunctionalExtensions;

namespace PetHelp.Domain.SpeciesManagement.Entities;

public class Breed : Entity<Guid>
{
    public Breed(
        Guid id,
        string name) : base(id)
    {
        Name = name;
    }

    public string Name { get; }
}