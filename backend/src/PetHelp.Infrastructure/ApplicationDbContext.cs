using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetHelp.Domain.AnimalManagement.AggregateRoot;
using PetHelp.Domain.SpeciesManagement.AgregateRoot;

namespace PetHelp.Infrastructure;

public class ApplicationDbContext(IConfiguration configuration) : DbContext, IApplicationDbContext
{
    private const string DATABASE = "Database"; 

    public DbSet<Volunteer> Volunteers => Set<Volunteer>();
    public DbSet<Species> Specieses => Set<Species>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(DATABASE));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
}