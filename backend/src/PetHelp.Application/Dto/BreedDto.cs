namespace PetHelp.Application.Dto;

public class BreedDto
{
    public Guid Id { get; init; }
    
    public Guid SpeciesId { get; init; }
    public string Title { get; init; } = String.Empty;
}