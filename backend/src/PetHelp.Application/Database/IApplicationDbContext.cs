using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using PetHelp.Domain.AnimalManagement.AggregateRoot;
using PetHelp.Domain.SpeciesManagement.AggregateRoot;

namespace PetHelp.Application.Database;

public interface IApplicationDbContext
{
    DbSet<Volunteer> Volunteers { get; }
    DbSet<Species> Species { get; }
    
    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken = default);
  
}