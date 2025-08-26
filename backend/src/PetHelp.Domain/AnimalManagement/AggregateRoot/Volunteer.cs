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
        var positionResult = Position.Create(_pets.Count + 1);
        if (positionResult.IsFailure)
            return positionResult.Error;

        pet.SetPosition(positionResult.Value);

        _pets.Add(pet);
        return Result.Success<Error>();
    }

    public UnitResult<Error> MovePet(Pet pet, Position newPosition)
    {
        var currentPosition = pet.Position;
        if (currentPosition == newPosition || _pets.Count == 1)
            return Result.Success<Error>();

        var adjustedPosition = AdjustNewPositionIfOutOfRange(newPosition);
        if (adjustedPosition.IsFailure)
            return adjustedPosition.Error;

        newPosition = adjustedPosition.Value;

        var moveResult = MovePetBetweenPosition(newPosition, currentPosition);
        if (moveResult.IsFailure)
            return moveResult.Error;

        pet.Move(newPosition);

        return Result.Success<Error>();
    }

    private UnitResult<Error> MovePetBetweenPosition(Position newPosition, Position currentPosition)
    {
        if (newPosition.Value < currentPosition.Value)
        {
            var petsToMove = _pets.Where(
                i => i.Position.Value >= newPosition.Value
                     && i.Position.Value < currentPosition.Value);

            foreach (var petToMove in petsToMove)
            {
                var result = petToMove.MoveForward();
                if (result.IsFailure)
                    return result.Error;
            }
        }
        else if (newPosition.Value > currentPosition.Value)
        {
            var petsToMove = _pets.Where(
                i => i.Position.Value > currentPosition.Value
                     && i.Position.Value <= newPosition.Value);

            foreach (var petToMove in petsToMove)
            {
                var result = petToMove.MoveBack();
                if (result.IsFailure)
                    return result.Error;
            }
        }

        return Result.Success<Error>();
    }

    private Result<Position, Error> AdjustNewPositionIfOutOfRange(Position newPosition)
    {
        if (newPosition.Value <= _pets.Count)
            return newPosition;

        var lastPosition = Position.Create(_pets.Count - 1);
        if (lastPosition.IsFailure)
            return lastPosition.Error;

        return lastPosition.Value;
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

    public Result<Pet, Error> GetPetById(PetId petId)
    {
        var pet = _pets.FirstOrDefault(i => i.Id == petId);
        if (pet is null)
            return Errors.General.NotFound(petId.Value);
        return pet;
    }
}