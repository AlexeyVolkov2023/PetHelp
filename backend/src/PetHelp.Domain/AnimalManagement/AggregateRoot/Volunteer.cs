using CSharpFunctionalExtensions;
using PetHelp.Domain.AnimalManagement.Entities;
using PetHelp.Domain.AnimalManagement.VO;
using PetHelp.Domain.Shared;

namespace PetHelp.Domain.AnimalManagement.AggregateRoot;

public class Volunteer : Entity<Guid>
{
    public Volunteer(
        Guid id,
        string fullName,
        string email,
        string description,
        int experienceInYears,
        string phoneNumber,
        List<SocialNetwork> socialNetworks,
        List<PaymentDetails> paymentDetails,
        List<Pet> ownedPets) : base(id)
    {
        FullName = fullName;
        Email = email;
        Description = description;
        ExperienceInYears = experienceInYears;
        PhoneNumber = phoneNumber;
        SocialNetworks = socialNetworks;
        PaymentDetails = paymentDetails;
        OwnedPets = ownedPets;
    }

    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string Description { get; private set; }
    public int ExperienceInYears { get; private set; }
    public string PhoneNumber { get; private set; }
    public List<SocialNetwork> SocialNetworks { get; private set; }
    public List<PaymentDetails> PaymentDetails { get; private set; }
    public List<Pet> OwnedPets { get; private set; }
    

    public int GetPetCountByStatus(string status)
    {
        return OwnedPets.Count(pet => pet.Status.Value == status);
    }
}