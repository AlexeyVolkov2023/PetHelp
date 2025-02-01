﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetHelp.Infrastructure;

#nullable disable

namespace PetHelp.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PetHelp.Domain.AnimalManagement.AggregateRoot.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("payment_details");

                    b.Property<string>("Networks")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("social+networks");

                    b.ComplexProperty<Dictionary<string, object>>("Description", "PetHelp.Domain.AnimalManagement.AggregateRoot.Volunteer.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(1000)
                                .HasColumnType("character varying(1000)")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Email", "PetHelp.Domain.AnimalManagement.AggregateRoot.Volunteer.Email#Email", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("email");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("ExperienceInYears", "PetHelp.Domain.AnimalManagement.AggregateRoot.Volunteer.ExperienceInYears#ExperienceInYears", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Value")
                                .HasMaxLength(100)
                                .HasColumnType("integer")
                                .HasColumnName("experience_in_years");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("FullName", "PetHelp.Domain.AnimalManagement.AggregateRoot.Volunteer.FullName#FullName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(15)
                                .HasColumnType("character varying(15)")
                                .HasColumnName("name");

                            b1.Property<string>("Patronymic")
                                .HasMaxLength(15)
                                .HasColumnType("character varying(15)")
                                .HasColumnName("patronymic");

                            b1.Property<string>("Surname")
                                .IsRequired()
                                .HasMaxLength(15)
                                .HasColumnType("character varying(15)")
                                .HasColumnName("surname");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PhoneNumber", "PetHelp.Domain.AnimalManagement.AggregateRoot.Volunteer.PhoneNumber#PhoneNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("phone_number");
                        });

                    b.HasKey("Id")
                        .HasName("pk_volunteers");

                    b.ToTable("volunteers", (string)null);
                });

            modelBuilder.Entity("PetHelp.Domain.AnimalManagement.Entities.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("payment_details");

                    b.Property<Guid>("volunteer_id")
                        .HasColumnType("uuid")
                        .HasColumnName("volunteer_id");

                    b.ComplexProperty<Dictionary<string, object>>("Address", "PetHelp.Domain.AnimalManagement.Entities.Pet.Address#Address", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int?>("Apartment")
                                .HasMaxLength(1600)
                                .HasColumnType("integer")
                                .HasColumnName("apartment");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(35)
                                .HasColumnType("character varying(35)")
                                .HasColumnName("city");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(35)
                                .HasColumnType("character varying(35)")
                                .HasColumnName("country");

                            b1.Property<string>("HouseNumber")
                                .IsRequired()
                                .HasMaxLength(6)
                                .HasColumnType("character varying(6)")
                                .HasColumnName("house_number");

                            b1.Property<string>("Region")
                                .IsRequired()
                                .HasMaxLength(35)
                                .HasColumnType("character varying(35)")
                                .HasColumnName("region");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(35)
                                .HasColumnType("character varying(35)")
                                .HasColumnName("street");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("OwnerPhoneNumber", "PetHelp.Domain.AnimalManagement.Entities.Pet.OwnerPhoneNumber#PhoneNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("owner_phone_number");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PetData", "PetHelp.Domain.AnimalManagement.Entities.Pet.PetData#PetData", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Color")
                                .IsRequired()
                                .HasMaxLength(15)
                                .HasColumnType("character varying(15)")
                                .HasColumnName("color");

                            b1.Property<string>("HealthInfo")
                                .IsRequired()
                                .HasMaxLength(250)
                                .HasColumnType("character varying(250)")
                                .HasColumnName("health_info");

                            b1.Property<double>("Height")
                                .HasMaxLength(200)
                                .HasColumnType("double precision")
                                .HasColumnName("Weight");

                            b1.Property<bool>("IsNeutered")
                                .HasColumnType("boolean")
                                .HasColumnName("is_neutered");

                            b1.Property<bool>("IsVaccinated")
                                .HasColumnType("boolean")
                                .HasColumnName("is_vaccinated");

                            b1.Property<double>("Weight")
                                .HasMaxLength(200)
                                .HasColumnType("double precision")
                                .HasColumnName("weight");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PetInfo", "PetHelp.Domain.AnimalManagement.Entities.Pet.PetInfo#PetInfo", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasMaxLength(1000)
                                .HasColumnType("character varying(1000)")
                                .HasColumnName("description");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(15)
                                .HasColumnType("character varying(15)")
                                .HasColumnName("name");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Status", "PetHelp.Domain.AnimalManagement.Entities.Pet.Status#PetStatus", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("status");
                        });

                    b.HasKey("Id")
                        .HasName("pk_pets");

                    b.HasIndex("volunteer_id")
                        .HasDatabaseName("ix_pets_volunteer_id");

                    b.ToTable("pets", (string)null);
                });

            modelBuilder.Entity("PetHelp.Domain.SpeciesManagement.AgregateRoot.Species", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Breeds")
                        .HasColumnType("text")
                        .HasColumnName("breeds");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_species");

                    b.ToTable("species", (string)null);
                });

            modelBuilder.Entity("PetHelp.Domain.SpeciesManagement.Entities.Breed", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_breeds");

                    b.ToTable("breeds", (string)null);
                });

            modelBuilder.Entity("PetHelp.Domain.AnimalManagement.Entities.Pet", b =>
                {
                    b.HasOne("PetHelp.Domain.AnimalManagement.AggregateRoot.Volunteer", "Volunteer")
                        .WithMany("Pets")
                        .HasForeignKey("volunteer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_pets_volunteers_volunteer_id");

                    b.OwnsOne("PetHelp.Domain.SpeciesManagement.VO.PetSpeciesBreed", "SpeciesBreed", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<Guid>("BreedId")
                                .HasColumnType("uuid")
                                .HasColumnName("breed_id");

                            b1.Property<Guid>("SpeciesId")
                                .HasColumnType("uuid")
                                .HasColumnName("species_id");

                            b1.HasKey("PetId");

                            b1.ToTable("pets");

                            b1.WithOwner()
                                .HasForeignKey("PetId")
                                .HasConstraintName("fk_pets_pets_id");
                        });

                    b.Navigation("SpeciesBreed")
                        .IsRequired();

                    b.Navigation("Volunteer");
                });

            modelBuilder.Entity("PetHelp.Domain.AnimalManagement.AggregateRoot.Volunteer", b =>
                {
                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}
