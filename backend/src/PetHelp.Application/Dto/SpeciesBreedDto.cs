namespace PetHelp.Application.Dto;

public class SpeciesBreedDto
{
    public Guid SpeciesId { get; init; } = Guid.Empty;
    public Guid BreedId { get; init; } = Guid.Empty;
}