using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelp.Application.Dto;
using PetHelp.Domain.SpeciesManagement.AggregateRoot;
using PetHelp.Domain.SpeciesManagement.Entities;
using PetHelp.Domain.SpeciesManagement.ID;
using PetHelp.Infrastructure.Extensions;

namespace PetHelp.Infrastructure.Configurations.Write;

public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> builder)
    {
        builder.ToTable("species");

        builder.HasKey(s => s.Id);

        builder.Property(a => a.Id)
            .HasConversion(
                id => id.Value,
                value => SpeciesId.Create(value));

        builder.Property(s => s.Title)
            .IsRequired()
            .HasMaxLength(Domain.Shared.Constants.TITLE_MAX_LENGTH);

        builder.HasMany(v => v.Breeds)
            .WithOne(p => p.Species)
            .HasForeignKey("species_id")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}