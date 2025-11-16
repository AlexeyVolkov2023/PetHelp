using CSharpFunctionalExtensions;
using PetHelp.Domain.Shared;
using PetHelp.Domain.SpeciesManagement.AggregateRoot;
using PetHelp.Domain.SpeciesManagement.ID;

namespace PetHelp.Application.SpeciesManagement;

public interface ISpeciesRepository
{
    Task<Result<Species, Error>> GetById(
        SpeciesId speciesId,
        CancellationToken cancellationToken = default);

    Task<Guid> Delete(
        Species species,
        CancellationToken cancellationToken = default);

}