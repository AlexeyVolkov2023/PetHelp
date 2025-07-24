using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using PetHelp.Domain.AnimalManagement.AggregateRoot;
using PetHelp.Domain.SpeciesManagement.AgregateRoot;

namespace PetHelp.Application.Database;

public interface IApplicationDbContext
{
    DbSet<Volunteer> Volunteers { get; }
    DbSet<Species> Specieses { get; }
    
    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken = default);
  
}