using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelp.Domain.AnimalManagement.Entities;
using PetHelp.Domain.AnimalManagement.ID;
using PetHelp.Domain.Shared;


namespace PetHelp.Infrastructure.Configuration;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id)
            .HasConversion(
                id => id.Value,
                value => PetId.Create(value));

        builder.Property(p => p.Name)
            .HasMaxLength(Constants.NAME_MAX_LENGTH)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(Constants.DESCRIPTION_MAX_LENGTH)
            .IsRequired();

        builder.Property(p => p.Color)
            .HasMaxLength(Constants.COLOR_MAX_LENGTH)
            .IsRequired();

        builder.Property(p => p.HealthInfo)
            .HasMaxLength(Constants.DESCRIPTION_MAX_LENGTH)
            .IsRequired();

        builder.Property(p => p.Address)
            .HasMaxLength(Constants.ADRESS_MAX_VALUE)
            .IsRequired();

        builder.Property(p => p.OwnerPhoneNumber)
            .HasMaxLength(Constants.PHONE_NUMBER_MAX_LENGTH)
            .IsRequired();

        builder.ComplexProperty(a => a.Status, sb =>
        {
            sb.Property(s => s.Value)
                .IsRequired();
        });

        builder.Property(p => p.Height)
            .HasMaxLength(Constants.HEIGHT_MAX_VALUE)
            .IsRequired();

        builder.Property(p => p.Weight)
            .HasMaxLength(Constants.WEIGHT_MAX_VALUE)
            .IsRequired();

        builder.Property(p => p.IsNeutered)
            .IsRequired();

        builder.Property(p => p.DateOfBirth)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        builder.Property(p => p.IsVaccinated)
            .IsRequired();

        builder.OwnsOne(p => p.SpeciesBreed, sb =>
        {
            sb.Property(s => s.SpeciesId)
                .IsRequired();
            sb.Property(s => s.BreedId)
                .IsRequired();
        });

        builder.Property(p => p.Details)
            .HasConversion(
                details => JsonSerializer.Serialize(details, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<IReadOnlyList<PaymentDetail>>(json, JsonSerializerOptions.Default)!,
                new ValueComparer<IReadOnlyList<PaymentDetail>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()));

        builder.Property(p => p.CreatedAt)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
    }
}