using CSharpFunctionalExtensions;
using PetHelp.Domain.AnimalManagement.Entities;
using PetHelp.Domain.AnimalManagement.ID;
using PetHelp.Domain.AnimalManagement.VO;
using PetHelp.Domain.Shared;

namespace PetHelp.Domain.AnimalManagement.AggregateRoot;

public class Volunteer : Entity<VolunteerId>
{
    private List<Pet> _pets = [];

    private Volunteer(VolunteerId id) : base(id)
    {
    }

    public Volunteer(
        VolunteerId id,
        string fullName,
        string email,
        string description,
        int experienceInYears,
        string phoneNumber,
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

    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string Description { get; private set; }
    public int ExperienceInYears { get; private set; }
    public string PhoneNumber { get; private set; }
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
        string fullName,
        string email,
        string description,
        int experienceInYears,
        string phoneNumber,
        IEnumerable<SocialNetwork> networks,
        IEnumerable<PaymentDetail> details)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            return Errors.General.ValueIsInvalid("FullName");
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            return Errors.General.ValueIsInvalid("Email");
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            return Errors.General.ValueIsInvalid("Description");
        }

        if (experienceInYears < 0)
        {
            return Errors.General.ValueIsRequired("ExperienceInYears");
        }

        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            return Errors.General.ValueIsInvalid("PhoneNumber");
        }

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