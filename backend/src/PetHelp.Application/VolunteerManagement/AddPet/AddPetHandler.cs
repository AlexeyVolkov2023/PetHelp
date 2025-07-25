using CSharpFunctionalExtensions;
using PetHelp.Application.Database;
using PetHelp.Application.FileProvider;
using PetHelp.Application.Providers;
using PetHelp.Domain.AnimalManagement.Entities;
using PetHelp.Domain.AnimalManagement.ID;
using PetHelp.Domain.AnimalManagement.VO;
using PetHelp.Domain.Shared;
using PetHelp.Domain.SpeciesManagement.VO;
using Microsoft.Extensions.Logging;

namespace PetHelp.Application.VolunteerManagement.AddPet;

public class AddPetHandler
{
    private const string BUCKET_NAME = "files";
    private readonly IFileProvider _fileProviders;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<AddPetHandler> _logger;

    public AddPetHandler(
        IFileProvider fileProvider,
        IVolunteersRepository volunteersRepository,
        IApplicationDbContext dbContext,
        ILogger<AddPetHandler> logger)
    {
        _fileProviders = fileProvider;
        _volunteersRepository = volunteersRepository;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        AddPetCommand command,
        CancellationToken cancellationToken)
    {
        var transaction = await _dbContext.BeginTransaction(cancellationToken);

        try
        {
            var volunteerResult = await _volunteersRepository.GetById(command.VolunteerId, cancellationToken);
            if (volunteerResult.IsFailure)
                return volunteerResult.Error.ToErrorList();
            var petId = PetId.NewPetId();

            var petInfo = PetInfo.Create(command.PetInfo.Name, command.PetInfo.Description).Value;

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

            var petSpeciesBreed = PetSpeciesBreed.Create(command.SpeciesId, command.BreedId).Value;

            List<FileData> filesData = [];

            foreach (var file in command.Files)
            {
                var extensions = Path.GetExtension(file.FileName);

                var filePath = FilePath.Create(Guid.NewGuid(), extensions);
                if (filePath.IsFailure)
                    return filePath.Error.ToErrorList();

                var fileContent = new FileData(file.Content, filePath.Value, BUCKET_NAME);

                filesData.Add(fileContent);
            }

            var petFiles = filesData
                .Select(f => f.FilePath)
                .Select(f => new PetFile(f))
                .ToList();

            var detailsResult = command.PaymentDetails
                .Select(d => PaymentDetail.Create(d.Title, d.Description).Value).ToList();


            var pet = new Pet(
                petId,
                petInfo,
                petData,
                address,
                phoneNumber,
                petStatus,
                dateOfBirth,
                petSpeciesBreed,
                petFiles,
                detailsResult);

            volunteerResult.Value.AddPet(pet);

            await _dbContext.SaveChangesAsync(cancellationToken);

            var uploadResult = await _fileProviders.UploadFiles(filesData, cancellationToken);
            if (uploadResult.IsFailure)
                return uploadResult.Error.ToErrorList();

            await transaction.CommitAsync(cancellationToken);

            return pet.Id.Value;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Can not add issue to module - {id} in transaction", command.VolunteerId);

            await transaction.RollbackAsync(cancellationToken);

            return Error
                .Failure("Can not add issue to module - {id}", "module.issue.failure")
                .ToErrorList();
        }

    }
}