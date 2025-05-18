using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PetHelp.Domain.AnimalManagement.AggregateRoot;
using PetHelp.Domain.SpeciesManagement.AgregateRoot;

namespace PetHelp.Infrastructure;

public interface IApplicationDbContext
{
    DbSet<Volunteer> Volunteers { get; }
    DbSet<Species> Specieses { get; }
    
    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}