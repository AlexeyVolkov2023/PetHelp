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

        builder.ComplexProperty(p => p.PetInfo,
            ib =>
            {
                ib.Property(f => f.Name)
                    .HasMaxLength(Constants.NAME_MAX_LENGTH)
                    .IsRequired(true)
                    .HasColumnName("name");
                ib.Property(f => f.Description)
                    .HasMaxLength(Constants.DESCRIPTION_MAX_LENGTH)
                    .IsRequired(true)
                    .HasColumnName("description");
            });

        builder.ComplexProperty(p => p.PetData,
            db =>
            {
                db.Property(d => d.Color)
                    .HasMaxLength(Constants.COLOR_MAX_LENGTH)
                    .IsRequired(true)
                    .HasColumnName("color");
                db.Property(d => d.HealthInfo)
                    .HasMaxLength(Constants.HEALTH_INFO_MAX_LENGTH)
                    .IsRequired(true)
                    .HasColumnName("health_info");
                db.Property(d => d.Height)
                    .HasMaxLength(Constants.HEIGHT_MAX_VALUE)
                    .IsRequired(true)
                    .HasColumnName("Weight");
                db.Property(d => d.Weight)
                    .HasMaxLength(Constants.WEIGHT_MAX_VALUE)
                    .IsRequired(true)
                    .HasColumnName("weight");
                db.Property(d => d.IsNeutered)
                    .IsRequired(true)
                    .HasColumnName("is_neutered"); 
                db.Property(d => d.IsVaccinated)
                    .IsRequired(true)
                    .HasColumnName("is_vaccinated");
            });

        builder.ComplexProperty(p => p.Address,
            ab =>
            {
                ab.Property(a => a.Country)
                    .HasMaxLength(Constants.ADDRESS_MAX_LENGTH)
                    .IsRequired(true)
                    .HasColumnName("country");
                ab.Property(a => a.Region)
                    .HasMaxLength(Constants.ADDRESS_MAX_LENGTH)
                    .IsRequired(true)
                    .HasColumnName("region");
                ab.Property(a => a.City)
                    .HasMaxLength(Constants.ADDRESS_MAX_LENGTH)
                    .IsRequired(true)
                    .HasColumnName("city");
                ab.Property(a => a.Street)
                    .HasMaxLength(Constants.ADDRESS_MAX_LENGTH)
                    .IsRequired(true)
                    .HasColumnName("street");
                ab.Property(a => a.HouseNumber)
                    .HasMaxLength(Constants.HOUS_NUMBER_MAX_VALUE)
                    .IsRequired(true)
                    .HasColumnName("house_number");
                ab.Property(a => a.Apartment)
                    .HasMaxLength(Constants.APPARTMENT_MAX_VALUE)
                    .IsRequired(false)
                    .HasColumnName("apartment");
                
            });

        builder.ComplexProperty(p => p.OwnerPhoneNumber,
            pb =>
            {
                pb.Property(n => n.Value)
                    .HasMaxLength(Constants.PHONE_NUMBER_MAX_LENGTH)
                    .IsRequired(true)
                    .HasColumnName("owner_phone_number");
            });

        builder.ComplexProperty(p => p.Status, sb =>
        {
            sb.Property(s => s.Value)
                .IsRequired(true)
                .HasColumnName("status");
        });

        builder.Property(p => p.DateOfBirth)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
            .IsRequired(true)
            .HasColumnName("date_of_birth");
      
        builder.OwnsOne(p => p.SpeciesBreed, sb =>
        {
            sb.Property(s => s.SpeciesId)
                .IsRequired(true)
                .HasColumnName("species_id");
            sb.Property(s => s.BreedId)
                .IsRequired(true)
                .HasColumnName("breed_id");
        });

        builder.Property(p => p.Details)
            .HasConversion(
                details => JsonSerializer.Serialize(details, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<IReadOnlyList<PaymentDetail>>(json, JsonSerializerOptions.Default)!,
                new ValueComparer<IReadOnlyList<PaymentDetail>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()))
            .HasColumnName("payment_details");

        builder.Property(p => p.CreatedAt)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
            .IsRequired(true)
            .HasColumnName("created_at");
   
    }
}