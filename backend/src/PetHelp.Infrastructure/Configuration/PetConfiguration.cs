using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelp.Domain.AnimalManagement.Entities;
using PetHelp.Domain.AnimalManagement.ID;
using PetHelp.Domain.AnimalManagement.VO;
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

        builder.ComplexProperty(p => p.PhoneNumber,
            pb =>
            {
                pb.Property(n => n.Value)
                    .HasMaxLength(Constants.PHONE_NUMBER_MAX_LENGTH)
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

        /*
        builder.Property(p => p.Files)
            .HasConversion(
                file => JsonSerializer.Serialize(file, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<IReadOnlyList<PetFile>>(json, JsonSerializerOptions.Default)!,
                new ValueComparer<IReadOnlyList<PetFile>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()))
            .HasColumnName("files");
            */
        builder.Property(p => p.Files)
            .HasConversion(
                files => JsonSerializer.Serialize(files, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Converters = { new FilePathJsonConverter() }
                }),
                json => JsonSerializer.Deserialize<IReadOnlyList<PetFile>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new FilePathJsonConverter() }
                }) ?? new List<PetFile>(),
                new ValueComparer<IReadOnlyList<PetFile>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()))
            .HasColumnType("jsonb")
            .HasColumnName("files");

        builder.Property(p => p.PaymentDetails)
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

        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}


public class FilePathJsonConverter : JsonConverter<FilePath>
{
    public override FilePath Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var path = reader.GetString();
        if (string.IsNullOrEmpty(path))
            throw new JsonException("FilePath cannot be null or empty");

        var parts = path.Split('.');
        if (parts.Length != 2)
            throw new JsonException("Invalid FilePath format");

        var result = FilePath.Create(Guid.Parse(parts[0]), parts[1]);
        if (result.IsFailure)
            throw new JsonException(result.Error.Message);

        return result.Value;
    }

    public override void Write(
        Utf8JsonWriter writer,
        FilePath value,
        JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Path);
    }
}