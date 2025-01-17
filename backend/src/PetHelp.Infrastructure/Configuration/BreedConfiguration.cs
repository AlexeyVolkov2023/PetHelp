using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelp.Domain.Shared;
using PetHelp.Domain.SpeciesManagement.Entities;
using PetHelp.Domain.SpeciesManagement.ID;

namespace PetHelp.Infrastructure.Configuration;

public class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breeds");

        builder.HasKey(b => b.Id);
        
        builder.Property(a => a.Id)
            .HasConversion(
                id => id.Value,
                value => BreedId.Create(value));

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(Constants.NAME_MAX_LENGTH);
    }
}