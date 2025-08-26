using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHelp.Application.Database;
using PetHelp.Application.Extensions;
using PetHelp.Application.FileProvider;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.VolunteerManagement.GetPresignedUrl;

public class GetPresignedUrlHandler
{
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<GetPresignedUrlHandler> _logger;
    private readonly IValidator<GetPresignedUrlCommand> _validator;

    public GetPresignedUrlHandler(
        IFileProvider fileProvider,
        ILogger<GetPresignedUrlHandler> logger, IValidator<GetPresignedUrlCommand> validator)
    {
        _fileProvider = fileProvider;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<string, ErrorList>> Handle(
        GetPresignedUrlCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var result = await _fileProvider.GetPresignedUrl(
            command.BucketName,
            command.ObjectName,
            command.ExpiryInSeconds,
            cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToErrorList();
        }
        
        _logger.LogInformation("Get presigned url for {ObjectName}", command.ObjectName);

        return result.Value;
    }
}