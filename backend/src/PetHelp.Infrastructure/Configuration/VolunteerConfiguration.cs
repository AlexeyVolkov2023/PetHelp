using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelp.Domain.AnimalManagement.AggregateRoot;
using PetHelp.Domain.AnimalManagement.ID;
using PetHelp.Domain.AnimalManagement.VO;
using PetHelp.Domain.Shared;

namespace PetHelp.Infrastructure.Configuration;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id)
            .HasConversion(
                id => id.Value,
                value => VolunteerId.Create(value));

        builder.Property(v => v.FullName)
            .HasMaxLength(Constants.FULL_NAME_MAX_LENGTH)
            .IsRequired();

        builder.Property(v => v.Email)
            .HasMaxLength(Constants.EMAIL_MAX_LENGTH)
            .IsRequired();

        builder.Property(v => v.Description)
            .HasMaxLength(Constants.DESCRIPTION_MAX_LENGTH)
            .IsRequired();

        builder.Property(v => v.ExperienceInYears)
            .HasMaxLength(Constants.EXPERIENCE_YEARS_MAX_VALUE)
            .IsRequired();

        builder.Property(v => v.PhoneNumber)
            .HasMaxLength(Constants.PHONE_NUMBER_MAX_LENGTH)
            .IsRequired();

        builder.Property(v => v.Details)
            .HasConversion(
                details => JsonSerializer.Serialize(details, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<IReadOnlyList<PaymentDetail>>(json, JsonSerializerOptions.Default)!,
                new ValueComparer<IReadOnlyList<PaymentDetail>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()));

        builder.Property(v => v.Networks)
            .HasConversion(
                networks => JsonSerializer.Serialize(networks, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<IReadOnlyList<SocialNetwork>>(json, JsonSerializerOptions.Default)!,
                new ValueComparer<IReadOnlyList<SocialNetwork>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()));
        
        builder.HasMany(v => v.Pets)
            .WithOne(p => p.Volunteer)
            .HasForeignKey("volunteer_id")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        builder.Navigation(v => v.Pets)
            .AutoInclude();
    }
}