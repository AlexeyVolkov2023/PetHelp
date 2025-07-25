using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelp.Application.Database;
using PetHelp.Application.Providers;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.VolunteerManagement.GetPresignedUrl;

public class GetPresignedUrlHandler
{
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<GetPresignedUrlHandler> _logger;

    public GetPresignedUrlHandler(
        IFileProvider fileProvider,
        ILogger<GetPresignedUrlHandler> logger)
    {
        _fileProvider = fileProvider;
        _logger = logger;
    }

    public async Task<Result<string, ErrorList>> Handle(
        GetPresignedUrlCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _fileProvider.GetPresignedUrl(
            command.BucketName,
            command.ObjectName,
            command.ExpiryInSeconds,
            cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToErrorList();
        }

        return result.Value;
    }
}