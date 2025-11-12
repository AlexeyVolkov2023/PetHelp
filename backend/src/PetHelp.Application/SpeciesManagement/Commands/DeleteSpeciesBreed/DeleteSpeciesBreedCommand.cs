using PetHelp.Application.Abstraction;

namespace PetHelp.Application.SpeciesManagement.Commands.DeleteSpeciesBreed;

public record DeleteSpeciesBreedCommand(
    Guid SpeciesId,
    Guid BreedId) : ICommand;
