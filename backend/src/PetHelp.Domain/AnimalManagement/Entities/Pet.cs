using System.Runtime.CompilerServices;
using CSharpFunctionalExtensions;
using PetHelp.Domain.AnimalManagement.AggregateRoot;
using PetHelp.Domain.AnimalManagement.ID;
using PetHelp.Domain.AnimalManagement.VO;
using PetHelp.Domain.Shared;
using PetHelp.Domain.SpeciesManagement.VO;

namespace PetHelp.Domain.AnimalManagement.Entities;

public class Pet : Entity<PetId>, ISoftDeletable
{
    public Volunteer Volunteer { get; private set; } = null!;
    private bool _isDeleted = false;

    public Pet(PetId id) : base(id)
    {
    }

    public Pet(
        PetId id,
        PetInfo petInfo,
        PetData petData,
        Address address,
        PhoneNumber phoneNumber,
        PetStatus status,
        DateOfBirth dateOfBirth,
        PetSpeciesBreed speciesBreed,
        IEnumerable<PetFile>? files,
        IEnumerable<PaymentDetail>? details) : base(id)
    {
        PetInfo = petInfo;
        PetData = petData;
        Address = address;
        PhoneNumber = phoneNumber;
        Status = status;
        DateOfBirth = dateOfBirth;
        SpeciesBreed = speciesBreed;
        Files = files?.ToList().AsReadOnly() ?? new List<PetFile>().AsReadOnly();
        PaymentDetails = details?.ToList() ?? [];
        CreatedAt = DateTime.UtcNow;
    }

    public PetInfo PetInfo { get; private set; }
    public PetData PetData { get; private set; }
    public Address Address { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public DateOfBirth DateOfBirth { get; private set; }
    public PetStatus Status { get; private set; }
    public IReadOnlyList<PaymentDetail> PaymentDetails { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public PetSpeciesBreed SpeciesBreed { get; private set; }
    public IReadOnlyList<PetFile> Files { get; private set; }
    public Position Position { get; private set; }

    public void SoftDelete()
    {
        if (_isDeleted == false)
            _isDeleted = true;
    }

    public void Restore()
    {
        if (_isDeleted)
            _isDeleted = false;
    }

    public void UpdateFiles(List<PetFile> files)
    {
        Files = files;
    }

    public void SetPosition(Position position) =>
        Position = position;

    public UnitResult<Error> MoveForward()
    {
        var newPosition = Position.Forward();
        if (newPosition.IsFailure)
            return newPosition.Error;

        Position = newPosition.Value;

        return Result.Success<Error>();
    }

    public UnitResult<Error> MoveBack()
    {
        var newPosition = Position.Back();
        if (newPosition.IsFailure)
            return newPosition.Error;

        Position = newPosition.Value;

        return Result.Success<Error>();
    }

    public void Move(Position newPosition) =>
        Position = newPosition;
}