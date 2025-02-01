using CSharpFunctionalExtensions;
using PetHelp.Domain.AnimalManagement.AggregateRoot;
using PetHelp.Domain.AnimalManagement.ID;
using PetHelp.Domain.AnimalManagement.VO;
using PetHelp.Domain.Shared;
using PetHelp.Domain.SpeciesManagement.VO;

namespace PetHelp.Domain.AnimalManagement.Entities;

public class Pet : Entity<PetId>
{
    public Volunteer Volunteer { get; private set; } = null!;

    public Pet(PetId id) : base(id)
    {
    }

    private Pet(
        PetId id,
        PetInfo petInfo,
        PetData petData,
        Address address,
        PhoneNumber ownerPhoneNumber,
        PetStatus status,
        DateTime dateOfBirth,
        PetSpeciesBreed speciesBreed,
        IEnumerable<PaymentDetail>? details = null) : base(id)
    {
        PetInfo = petInfo;
        PetData = petData;
        Address = address;
        OwnerPhoneNumber = ownerPhoneNumber;
        Status = status;
        DateOfBirth = dateOfBirth;
        SpeciesBreed = speciesBreed;
        Details = details?.ToList() ?? [];
        CreatedAt = DateTime.UtcNow;
    }

    public PetInfo PetInfo { get; private set; }
    public PetData PetData { get; private set; }
    public Address Address { get; private set; }
    public PhoneNumber OwnerPhoneNumber { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public PetStatus Status { get; private set; }
    public IReadOnlyList<PaymentDetail> Details { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public PetSpeciesBreed SpeciesBreed { get; private set; }

    public static Result<Pet, Error> Create(
        PetId id,
        PetInfo petInfo,
        PetData petData,
        Address address,
        PhoneNumber ownerPhoneNumber,
        PetStatus petStatus,
        DateTime dateOfBirth,
        PetSpeciesBreed speciesBreed,
        IEnumerable<PaymentDetail>? details = null)
    {
        if (dateOfBirth > DateTime.UtcNow)
        {
            return Errors.General.ValueIsRequired("DateOfBirth");
        }

        return new Pet(
            id,
            petInfo,
            petData,
            address,
            ownerPhoneNumber,
            petStatus,
            dateOfBirth,
            speciesBreed,
            details);
    }
}