using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHelp.Application.Database;
using PetHelp.Application.Extensions;
using PetHelp.Application.FileProvider;
using PetHelp.Domain.AnimalManagement.ID;
using PetHelp.Domain.AnimalManagement.VO;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.VolunteerManagement.UploadFilesToPet;

public class UploadFilesToPetHandler
{
    private const string BUCKET_NAME = "files";

    private readonly IFileProvider _fileProviders;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<UploadFilesToPetHandler> _logger;
    private readonly IValidator<UploadFilesToPetCommand> _validator;

    public UploadFilesToPetHandler(
        IFileProvider fileProvider,
        IVolunteersRepository volunteersRepository,
        IApplicationDbContext dbContext,
        ILogger<UploadFilesToPetHandler> logger,
        IValidator<UploadFilesToPetCommand> validator)
    {
        _fileProviders = fileProvider;
        _volunteersRepository = volunteersRepository;
        _dbContext = dbContext;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UploadFilesToPetCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerResult = await _volunteersRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var petId = PetId.Create(command.PetId);

        var petResult = volunteerResult.Value.GetPetById(petId);
        if (petResult.IsFailure)
            return petResult.Error.ToErrorList();

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

        var filePathsResult = await _fileProviders.UploadFiles(filesData, cancellationToken);
        if (filePathsResult.IsFailure)
            return filePathsResult.Error.ToErrorList();

        var petFiles = filePathsResult.Value
            .Select(f => new PetFile(f))
            .ToList();

        petResult.Value.UpdateFiles(petFiles);

        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Success uploaded files to pet {id}", petResult.Value.Id.Value);

        return petResult.Value.Id.Value;
    }
}