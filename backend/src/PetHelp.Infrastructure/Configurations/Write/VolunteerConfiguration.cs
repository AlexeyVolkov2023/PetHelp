using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelp.Application.Dto;
using PetHelp.Domain.AnimalManagement.AggregateRoot;
using PetHelp.Domain.AnimalManagement.ID;
using PetHelp.Domain.AnimalManagement.VO;
using PetHelp.Domain.Shared;
using PetHelp.Infrastructure.Extensions;

namespace PetHelp.Infrastructure.Configurations.Write;

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

        builder.ComplexProperty(v => v.FullName,
            fb =>
            {
                fb.Property(f => f.Name)
                    .HasMaxLength(Domain.Shared.Constants.NAME_MAX_LENGTH)
                    .IsRequired(true)
                    .HasColumnName("name");
                fb.Property(f => f.Surname)
                    .HasMaxLength(Domain.Shared.Constants.SURNAME_MAX_LENGTH)
                    .IsRequired(true)
                    .HasColumnName("surname");
                fb.Property(f => f.Patronymic)
                    .HasMaxLength(Domain.Shared.Constants.NAME_MAX_LENGTH)
                    .IsRequired(false)
                    .HasColumnName("patronymic");
            });

        builder.ComplexProperty(v => v.Email,
            eb =>
            {
                eb.Property(e => e.Value)
                    .HasMaxLength(Domain.Shared.Constants.EMAIL_MAX_LENGTH)
                    .IsRequired(true)
                    .HasColumnName("email");
            });

        builder.ComplexProperty(v => v.Description,
            db =>
            {
                db.Property(e => e.Value)
                    .HasMaxLength(Domain.Shared.Constants.DESCRIPTION_MAX_LENGTH)
                    .IsRequired(true)
                    .HasColumnName("description");
            });

        builder.ComplexProperty(v => v.ExperienceInYears,
            eb =>
            {
                eb.Property(e => e.Value)
                    .HasMaxLength(Domain.Shared.Constants.EXPERIENCE_YEARS_MAX_VALUE)
                    .IsRequired(true)
                    .HasColumnName("experience_in_years");
            });

        builder.ComplexProperty(v => v.PhoneNumber,
            pb =>
            {
                pb.Property(n => n.Value)
                    .HasMaxLength(Domain.Shared.Constants.PHONE_NUMBER_MAX_LENGTH)
                    .IsRequired(true)
                    .HasColumnName("phone_number");
            });

        builder.Property(p => p.PaymentDetails)
            .ValueObjectsCollectionJsonConversion(
                details => new PaymentDetailDto( details.Title, details.Description),
                dto => PaymentDetail.Create(dto.Title, dto.Description).Value)
            .HasColumnName("payment_details");
        
       builder.Property(v => v.SocialNetworks)
           .ValueObjectsCollectionJsonConversion(
               networks => new SocialNetworkDto( networks.Name, networks.Link),
               dto => SocialNetwork.Create(dto.Name, dto.Link).Value)
            .HasColumnName("social_networks");

        builder.HasMany(v => v.Pets)
            .WithOne(p => p.Volunteer)
            .HasForeignKey("volunteer_id")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");

        builder.Navigation(v => v.Pets)
            .AutoInclude();
    }
}