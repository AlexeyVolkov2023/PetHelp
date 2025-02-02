using CSharpFunctionalExtensions;
using PetHelp.Domain.AnimalManagement.Entities;
using PetHelp.Domain.AnimalManagement.ID;
using PetHelp.Domain.AnimalManagement.VO;
using PetHelp.Domain.Shared;

namespace PetHelp.Domain.AnimalManagement.AggregateRoot;

public class Volunteer : Entity<VolunteerId>
{
    private readonly List<Pet> _pets = [];

    private Volunteer(VolunteerId id) : base(id)
    {
    }

    public Volunteer(
        VolunteerId id,
        FullName fullName,
        Email email,
        Description description,
        ExperienceInYears experienceInYears,
        PhoneNumber phoneNumber,
        IEnumerable<PaymentDetail>? details,
        IEnumerable<SocialNetwork>? networks) : base(id)
    {
        FullName = fullName;
        Email = email;
        Description = description;
        ExperienceInYears = experienceInYears;
        PhoneNumber = phoneNumber;
        Details = details?.ToList() ?? [];
        Networks = networks?.ToList() ?? [];
    }

    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public Description Description { get; private set; }
    public ExperienceInYears ExperienceInYears { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public IReadOnlyList<SocialNetwork> Networks { get; private set; }
    public IReadOnlyList<PaymentDetail> Details { get; private set; }
    public IReadOnlyList<Pet> Pets => _pets;

    public int GetPetCountByStatus(string status)
    {
        return Pets.Count(pet => pet.Status.Value == status);
    }

    public void AddOwnedPet(Pet pet)
    {
        _pets.Add(pet);
    }

    public static Result<Volunteer, Error> Create(
        VolunteerId id,
        FullName fullName,
        Email email,
        Description description,
        ExperienceInYears experienceInYears,
        PhoneNumber phoneNumber,
        IEnumerable<SocialNetwork> networks,
        IEnumerable<PaymentDetail> details)
    {
        return new Volunteer(
            id,
            fullName,
            email,
            description,
            experienceInYears,
            phoneNumber,
            details,
            networks);
    }
}