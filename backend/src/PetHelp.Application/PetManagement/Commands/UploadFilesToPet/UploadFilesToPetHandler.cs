using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHelp.Application.Abstraction;
using PetHelp.Application.Database;
using PetHelp.Application.Extensions;
using PetHelp.Application.Files;
using PetHelp.Application.Messaging;
using PetHelp.Domain.AnimalManagement.ID;
using PetHelp.Domain.AnimalManagement.VO;
using PetHelp.Domain.Shared;
using FileInfo = PetHelp.Application.Files.FileInfo;

namespace PetHelp.Application.PetManagement.Commands.UploadFilesToPet;

public class UploadFilesToPetHandler : ICommandHandler<Guid, UploadFilesToPetCommand>
{
    private const string BUCKET_NAME = "files";

    private readonly IFileProvider _fileProviders;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IApplicationDbContext _dbContext;
    private readonly IMessageQueue<IEnumerable<FileInfo>> _messageQueue;
    private readonly ILogger<UploadFilesToPetHandler> _logger;
    private readonly IValidator<UploadFilesToPetCommand> _validator;

    public UploadFilesToPetHandler(
        IFileProvider fileProvider,
        IVolunteersRepository volunteersRepository,
        IApplicationDbContext dbContext,
        IMessageQueue<IEnumerable<FileInfo>> messageQueue,
        ILogger<UploadFilesToPetHandler> logger,
        IValidator<UploadFilesToPetCommand> validator)
    {
        _fileProviders = fileProvider;
        _volunteersRepository = volunteersRepository;
        _dbContext = dbContext;
        _messageQueue = messageQueue;
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

            var fileInfo = new FileInfo(filePath.Value, BUCKET_NAME);

            var fileContent = new FileData(file.Content, fileInfo);

            filesData.Add(fileContent);
        }

        var filePathsResult = await _fileProviders.UploadFiles(filesData, cancellationToken);
        if (filePathsResult.IsFailure)
        {
            await _messageQueue.WriteAsync(filesData.Select(f => f.FileInfo),cancellationToken);
            return filePathsResult.Error.ToErrorList();
        }
            

        var petFiles = filePathsResult.Value
            .Select(f => new PetFile(f))
            .ToList();

        petResult.Value.UpdateFiles(petFiles);

        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Success uploaded files to pet {id}", petResult.Value.Id.Value);

        return petResult.Value.Id.Value;
    }
}