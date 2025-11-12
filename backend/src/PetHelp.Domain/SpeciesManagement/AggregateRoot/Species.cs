using CSharpFunctionalExtensions;
using PetHelp.Domain.Shared;
using PetHelp.Domain.SpeciesManagement.Entities;
using PetHelp.Domain.SpeciesManagement.ID;

namespace PetHelp.Domain.SpeciesManagement.AggregateRoot;

public class Species : Entity<SpeciesId>
{
    private readonly List<Breed> _breeds = [];

    private Species(SpeciesId id) : base(id)
    {
    }

    private Species(
        SpeciesId id,
        string title,
        IEnumerable<Breed>? breeds) : base(id)
    {
        Title = title;
    }

    public string Title { get; }
    public IReadOnlyList<Breed> Breeds => _breeds;

    public static Result<Species, Error> Create(
        SpeciesId id,
        string title,
        IEnumerable<Breed>? breeds)
    {
        if (string.IsNullOrWhiteSpace(title))
            return Errors.General.ValueIsInvalid("title");

        return new Species(id, title, breeds);
    }

    public Result<Breed, Error> GetBreedById(BreedId breedId)
    {
        var breed = _breeds.FirstOrDefault(i => i.Id == breedId);
        if (breed is null)
            return Errors.General.NotFound(breedId.Value);
        return breed;
    }

    public Result<Guid, Error> RemoveBreed(BreedId breedId)
    {
        var breed = GetBreedById(breedId);

        _breeds.Remove(breed.Value);
        return breedId.Value;
    }
}