using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelp.Application.Dto;
using PetHelp.Domain.Shared;

namespace PetHelp.Infrastructure.Configurations.Read;

public class PetDtoConfiguration : IEntityTypeConfiguration<PetDto>
{
    public void Configure(EntityTypeBuilder<PetDto> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(v => v.Id);

        builder.ComplexProperty(p => p.PetInfo, ib =>
        {
            ib.Property(f => f.Name)
                .HasColumnName("name");
            ib.Property(f => f.Description)
                .HasColumnName("description");
        });

        builder.ComplexProperty(p => p.PetData, db =>
        {
            db.Property(d => d.Color)
                .HasColumnName("color");
            db.Property(d => d.HealthInfo)
                .HasColumnName("health_info");
            db.Property(d => d.Height)
                .HasColumnName("Weight");
            db.Property(d => d.Weight)
                .HasColumnName("weight");
            db.Property(d => d.IsNeutered)
                .HasColumnName("is_neutered");
            db.Property(d => d.IsVaccinated)
                .HasColumnName("is_vaccinated");
        });

        builder.ComplexProperty(p => p.Address, ab =>
        {
            ab.Property(a => a.Country)
                .HasColumnName("country");
            ab.Property(a => a.Region)
                .HasColumnName("region");
            ab.Property(a => a.City)
                .HasColumnName("city");
            ab.Property(a => a.Street)
                .HasColumnName("street");
            ab.Property(a => a.HouseNumber)
                .HasColumnName("house_number");
            ab.Property(a => a.Apartment)
                .HasColumnName("apartment");
        });

        builder.Property(p => p.PhoneNumber)
                    .HasColumnName("owner_phone_number");

        builder.Property(p => p.Status)
            .HasColumnName("status");

        builder.Property(p => p.DateOfBirth)
                .HasColumnName("date_of_birth");

        builder.OwnsOne(p => p.SpeciesBreed, sb =>
        {
            sb.Property(s => s.SpeciesId)
                .HasColumnName("species_id");
            sb.Property(s => s.BreedId)
                .HasColumnName("breed_id");
        });

        builder.Property(p => p.Files)
            .HasConversion(
                file => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<PetFileDto[]>(json, JsonSerializerOptions.Default)!);

        builder.Property(p => p.PaymentDetails)
            .HasConversion(
                details => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<PaymentDetailDto[]>(json, JsonSerializerOptions.Default)!);
               

        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at");

        builder.Property(p => p.Position)
                .HasColumnName("position");
      
    }
}