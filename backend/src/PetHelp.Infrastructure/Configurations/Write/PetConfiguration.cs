using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelp.Application.Dto;
using PetHelp.Domain.AnimalManagement.Entities;
using PetHelp.Domain.AnimalManagement.ID;
using PetHelp.Domain.AnimalManagement.VO;
using PetHelp.Domain.Shared;
using PetHelp.Infrastructure.Extensions;

namespace PetHelp.Infrastructure.Configurations.Write;

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
                    .HasMaxLength(Domain.Shared.Constants.NAME_MAX_LENGTH)
                    .IsRequired(true)
                    .HasColumnName("name");
                ib.Property(f => f.Description)
                    .HasMaxLength(Domain.Shared.Constants.DESCRIPTION_MAX_LENGTH)
                    .IsRequired(true)
                    .HasColumnName("description");
            });

        builder.ComplexProperty(p => p.PetData,
            db =>
            {
                db.Property(d => d.Color)
                    .HasMaxLength(Domain.Shared.Constants.COLOR_MAX_LENGTH)
                    .IsRequired(true)
                    .HasColumnName("color");
                db.Property(d => d.HealthInfo)
                    .HasMaxLength(Domain.Shared.Constants.HEALTH_INFO_MAX_LENGTH)
                    .IsRequired(true)
                    .HasColumnName("health_info");
                db.Property(d => d.Height)
                    .HasMaxLength(Domain.Shared.Constants.HEIGHT_MAX_VALUE)
                    .IsRequired(true)
                    .HasColumnName("Weight");
                db.Property(d => d.Weight)
                    .HasMaxLength(Domain.Shared.Constants.WEIGHT_MAX_VALUE)
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
                    .HasMaxLength(Domain.Shared.Constants.ADDRESS_MAX_LENGTH)
                    .IsRequired(true)
                    .HasColumnName("country");
                ab.Property(a => a.Region)
                    .HasMaxLength(Domain.Shared.Constants.ADDRESS_MAX_LENGTH)
                    .IsRequired(true)
                    .HasColumnName("region");
                ab.Property(a => a.City)
                    .HasMaxLength(Domain.Shared.Constants.ADDRESS_MAX_LENGTH)
                    .IsRequired(true)
                    .HasColumnName("city");
                ab.Property(a => a.Street)
                    .HasMaxLength(Domain.Shared.Constants.ADDRESS_MAX_LENGTH)
                    .IsRequired(true)
                    .HasColumnName("street");
                ab.Property(a => a.HouseNumber)
                    .HasMaxLength(Domain.Shared.Constants.HOUS_NUMBER_MAX_VALUE)
                    .IsRequired(true)
                    .HasColumnName("house_number");
                ab.Property(a => a.Apartment)
                    .HasMaxLength(Domain.Shared.Constants.APPARTMENT_MAX_VALUE)
                    .IsRequired(false)
                    .HasColumnName("apartment");
            });

        builder.ComplexProperty(p => p.PhoneNumber,
            pb =>
            {
                pb.Property(n => n.Value)
                    .HasMaxLength(Domain.Shared.Constants.PHONE_NUMBER_MAX_LENGTH)
                    .IsRequired(true)
                    .HasColumnName("owner_phone_number");
            });

        builder.ComplexProperty(p => p.Status, sb =>
        {
            sb.Property(s => s.Status)
                .IsRequired(true)
                .HasColumnName("status");
        });

        builder.ComplexProperty(p => p.DateOfBirth, dob =>
        {
            dob.Property(d => d.Date)
                .IsRequired(true)
                .HasColumnName("date_of_birth")
                .HasConversion(
                    v => v.ToUniversalTime(),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        });

        builder.OwnsOne(p => p.SpeciesBreed, sb =>
        {
            sb.Property(s => s.SpeciesId)
                .IsRequired(true)
                .HasColumnName("species_id");
            sb.Property(s => s.BreedId)
                .IsRequired(true)
                .HasColumnName("breed_id");
        });

        builder.Property(p => p.Files)
            .ValueObjectsCollectionJsonConversion(
                file => new PetFileDto{ PathToStorage = file.PathToStorage.Path},
                dto => new PetFile(FilePath.Create(dto.PathToStorage).Value))
            .HasColumnName("files");

        builder.Property(p => p.PaymentDetails)
            .ValueObjectsCollectionJsonConversion(
                details => new PaymentDetailDto( details.Title, details.Description),
                dto => PaymentDetail.Create(dto.Title, dto.Description).Value)
            .HasColumnName("payment_details");
        
        builder.Property(p => p.CreatedAt)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
            .IsRequired(true)
            .HasColumnName("created_at");

        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");

        builder.ComplexProperty(p => p.Position, sb =>
        {
            sb.Property(s => s.Value)
                .IsRequired()
                .HasColumnName("position");
        });
    }
}