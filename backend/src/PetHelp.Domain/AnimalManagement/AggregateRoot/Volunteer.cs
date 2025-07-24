using CSharpFunctionalExtensions;
using PetHelp.Domain.AnimalManagement.Entities;
using PetHelp.Domain.AnimalManagement.ID;
using PetHelp.Domain.AnimalManagement.VO;
using PetHelp.Domain.Shared;

namespace PetHelp.Domain.AnimalManagement.AggregateRoot;

public class Volunteer : Entity<VolunteerId>, ISoftDeletable
{
    private readonly List<Pet> _pets = [];
    private bool _isDeleted = false;

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
        PaymentDetails = details?.ToList() ?? [];
        SocialNetworks = networks?.ToList() ?? [];
    }

    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public Description Description { get; private set; }
    public ExperienceInYears ExperienceInYears { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public IReadOnlyList<SocialNetwork> SocialNetworks { get; private set; }
    public IReadOnlyList<PaymentDetail> PaymentDetails { get; private set; }
    public IReadOnlyList<Pet> Pets => _pets;

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

    public int GetPetCountByStatus(string status)
    {
        return Pets.Count(pet => pet.Status.Status == status);
    }

    public UnitResult<Error> AddPet(Pet pet)
    {
        _pets.Add(pet);
        return Result.Success<Error>();
    } 
   

    public void UpdateMainInfo(
        FullName fullName,
        Email email,
        Description description,
        ExperienceInYears experienceInYears,
        PhoneNumber phoneNumber)
    {
        FullName = fullName;
        Email = email;
        Description = description;
        ExperienceInYears = experienceInYears;
        PhoneNumber = phoneNumber;
    }

    public void UpdateSocialNetworks(IEnumerable<SocialNetwork> newSocialNetworks)
    {
        SocialNetworks = newSocialNetworks.ToList();
    }

    public void UpdatePaymentDetail(IEnumerable<PaymentDetail> newPaymentDetails)
    {
        PaymentDetails = newPaymentDetails.ToList();
    }

    public void SoftDelete()
    {
        if (_isDeleted == false)
            _isDeleted = true;

        foreach (var pet in _pets)
        {
            pet.SoftDelete();
        }
    }

    public void Restore()
    {
        if (_isDeleted)
            _isDeleted = false;
        foreach (var pet in _pets)
        {
            pet.Restore();
        }
    }
}