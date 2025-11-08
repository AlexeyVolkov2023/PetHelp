using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHelp.Application.Abstraction;
using PetHelp.Application.Extensions;
using PetHelp.Application.Files;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.PetManagement.Commands.RemoveFile;

public class RemoveFileHandler : ICommandHandler<RemoveFileCommand> 
{
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<RemoveFileHandler> _logger;
    private readonly IValidator<RemoveFileCommand> _validator;

    public RemoveFileHandler(
        IFileProvider fileProvider,
        ILogger<RemoveFileHandler> logger, IValidator<RemoveFileCommand> validator)
    {
        _fileProvider = fileProvider;
        _logger = logger;
        _validator = validator;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        RemoveFileCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var result = await _fileProvider.RemoveFile(
            command.BucketName,
            command.ObjectName,
            cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToErrorList();
        }
        
        _logger.LogInformation("Remove object with {ObjectName}", command.ObjectName);

        return UnitResult.Success<ErrorList>();
    }
}