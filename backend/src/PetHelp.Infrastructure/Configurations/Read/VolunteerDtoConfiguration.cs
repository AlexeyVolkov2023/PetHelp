using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelp.Application.Dto;
using PetHelp.Domain.AnimalManagement.VO;

namespace PetHelp.Infrastructure.Configurations.Read;

public class VolunteerDtoConfiguration : IEntityTypeConfiguration<VolunteerDto> {
    public void Configure(EntityTypeBuilder<VolunteerDto> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(v => v.Id);
        
        builder.ComplexProperty(v => v.FullName,
            fb =>
            {
                fb.Property(f => f.Name)
                    .HasColumnName("name");
                fb.Property(f => f.Surname)
                    .HasColumnName("surname");
                fb.Property(f => f.Patronymic)
                   .HasColumnName("patronymic");
            });

        builder.Property(v => v.Email)
           .HasColumnName("email");

        builder.Property(v => v.Description)
            .HasColumnName("description");

        builder.Property(v => v.ExperienceInYears)
          .HasColumnName("experience_in_years");

        builder.Property(v => v.PhoneNumber)
            .HasColumnName("phone_number");
        
        builder.Property(p => p.PaymentDetails)
            .HasConversion(
                details => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<PaymentDetailDto[]>(json, JsonSerializerOptions.Default)!)
            .HasColumnName("payment_details");
        
        builder.Property(v => v.SocialNetworks)
            .HasConversion(
                network => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<SocialNetworkDto[]>(json, JsonSerializerOptions.Default)!)
            .HasColumnName("social_networks");
        
        
    }
}