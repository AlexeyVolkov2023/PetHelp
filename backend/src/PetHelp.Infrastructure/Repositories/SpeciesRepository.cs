using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetHelp.Application.Database;
using PetHelp.Application.SpeciesManagement;
using PetHelp.Domain.Shared;
using PetHelp.Domain.SpeciesManagement.AggregateRoot;
using PetHelp.Domain.SpeciesManagement.ID;

namespace PetHelp.Infrastructure.Repositories;

public class SpeciesRepository(
    IApplicationDbContext dbContext) : ISpeciesRepository
{
    public async Task<Result<Species, Error>> GetById(
        SpeciesId speciesId,
        CancellationToken cancellationToken = default)
    {
        var species = await dbContext.Species
            .Include(v => v.Breeds)
            .FirstOrDefaultAsync(v => v.Id == speciesId, cancellationToken);
        if (species is null)
            return Errors.General.NotFound(speciesId);

        return species;
    }
    
    public async Task<Guid> Delete(
        Species species,
        CancellationToken cancellationToken = default)
    {
        dbContext.Species.Remove(species);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return species.Id;
    }

}

