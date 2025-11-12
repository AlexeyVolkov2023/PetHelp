using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetHelp.Application.Abstraction;
using PetHelp.Application.Database;
using PetHelp.Domain.AnimalManagement.Entities;
using PetHelp.Domain.AnimalManagement.ID;
using PetHelp.Domain.AnimalManagement.VO;
using PetHelp.Domain.Shared;
using PetHelp.Domain.SpeciesManagement.VO;

namespace PetHelp.Application.PetManagement.Commands.AddPet;

public class AddPetHandler : ICommandHandler<Guid, AddPetCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IApplicationDbContext _dbContext;
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<AddPetHandler> _logger;
    private readonly IValidator<AddPetCommand> _validator;

    public AddPetHandler(
        IVolunteersRepository volunteersRepository,
        IApplicationDbContext dbContext,
        IReadDbContext readDbContext,
        ILogger<AddPetHandler> logger,
        IValidator<AddPetCommand> validator)
    {
        _volunteersRepository = volunteersRepository;
        _dbContext = dbContext;
        _readDbContext = readDbContext;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        AddPetCommand command,
        CancellationToken cancellationToken)
    {
        var volunteerResult = await _volunteersRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        var species =await _readDbContext.Species
            .FirstOrDefaultAsync(s => s.Title == command.Species, cancellationToken);
        if (species == null)
            return Errors.Species.NotFound();

        var breed = await _readDbContext.Breeds
            .FirstOrDefaultAsync(b => b.SpeciesId == species.Id && b.Title == command.Breed, cancellationToken);
        if (breed == null)
            return Errors.Breed.NotFound();

        var pet = InitPet(command, species.Id, breed.Id);

        volunteerResult.Value.AddPet(pet);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return pet.Id.Value;
    }

    private static Pet InitPet(AddPetCommand command, Guid speciesId, Guid breedId)
    {
        var petId = PetId.NewPetId();

        var petInfo = PetInfo.Create(
            command.PetInfo.Name,
            command.PetInfo.Description).Value;

        var petData = PetData.Create(
            command.PetData.Color,
            command.PetData.HealthInfo,
            command.PetData.Weight,
            command.PetData.Height,
            command.PetData.IsNeutered,
            command.PetData.IsVaccinated).Value;

        var address = Address.Create(
            command.Address.Country,
            command.Address.Region,
            command.Address.City,
            command.Address.Street,
            command.Address.HouseNumber,
            command.Address.Apartment).Value;

        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;

        var petStatus = PetStatus.Create(command.Status).Value;

        var dateOfBirth = DateOfBirth.Create(command.DateOfBirth).Value;
        
        

        var petSpeciesBreed = PetSpeciesBreed.Create(speciesId, breedId).Value;

        var detailsResult = command.PaymentDetails
            .Select(d => PaymentDetail.Create(d.Title, d.Description).Value).ToList();

        return new Pet(
            petId,
            petInfo,
            petData,
            address,
            phoneNumber,
            petStatus,
            dateOfBirth,
            petSpeciesBreed,
            null,
            detailsResult);
    }
}