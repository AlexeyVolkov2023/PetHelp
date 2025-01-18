using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelp.Domain.Shared;
using PetHelp.Domain.SpeciesManagement.AgregateRoot;
using PetHelp.Domain.SpeciesManagement.Entities;
using PetHelp.Domain.SpeciesManagement.ID;

namespace PetHelp.Infrastructure.Configuration;

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
            .HasMaxLength(Constants.TITLE_MAX_LENGTH);

        builder.Property(v => v.Breeds)
            .HasConversion(
                breeds => JsonSerializer.Serialize(breeds, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<IReadOnlyList<Breed>>(json, JsonSerializerOptions.Default)!,
                new ValueComparer<IReadOnlyList<Breed>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()));
    }
}