using CSharpFunctionalExtensions;
using PetHelp.Domain.AnimalManagement.AggregateRoot;
using PetHelp.Domain.AnimalManagement.ID;
using PetHelp.Domain.AnimalManagement.VO;
using PetHelp.Domain.Shared;
using PetHelp.Domain.SpeciesManagement.VO;

namespace PetHelp.Domain.AnimalManagement.Entities;

public class Pet : Entity<PetId>
{
    // ef navigation
    public Volunteer Volunteer { get; private set; } = null!;

    //ef
    public Pet(PetId id) : base(id)
    {
    }

    private Pet(
        PetId id,
        string name,
        string description,
        string color,
        string healthInfo,
        string address,
        string ownerPhoneNumber,
        PetStatus status,
        double height,
        double weight,
        bool isNeutered,
        DateTime dateOfBirth,
        bool isVaccinated,
        PetSpeciesBreed speciesBreed,
        IEnumerable<PaymentDetail>? details = null) : base(id)
    {
        Name = name;
        Description = description;
        Color = color;
        HealthInfo = healthInfo;
        Address = address;
        OwnerPhoneNumber = ownerPhoneNumber;
        Status = status;
        Height = height;
        Weight = weight;
        IsNeutered = isNeutered;
        DateOfBirth = dateOfBirth;
        IsVaccinated = isVaccinated;
        SpeciesBreed = speciesBreed;
        Details = details?.ToList() ?? [];
        CreatedAt = DateTime.UtcNow;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Color { get; private set; }
    public string HealthInfo { get; private set; }
    public string Address { get; private set; }
    public double Weight { get; private set; }
    public double Height { get; private set; }
    public string OwnerPhoneNumber { get; private set; }
    public bool IsNeutered { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public bool IsVaccinated { get; private set; }
    public PetStatus Status { get; private set; }
    public IReadOnlyList<PaymentDetail> Details { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public PetSpeciesBreed SpeciesBreed { get; private set; }
}