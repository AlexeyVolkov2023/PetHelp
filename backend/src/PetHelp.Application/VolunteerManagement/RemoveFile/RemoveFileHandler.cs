using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelp.Application.Providers;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.VolunteerManagement.RemoveFile;

public class RemoveFileHandler
{
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<RemoveFileHandler> _logger;

    public RemoveFileHandler(
        IFileProvider fileProvider,
        ILogger<RemoveFileHandler> logger)
    {
        _fileProvider = fileProvider;
        _logger = logger;
    }

    public async Task<Result<string, ErrorList>> Handle(
        RemoveFileCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _fileProvider.RemoveFile(
            command.BucketName,
            command.ObjectName,
            cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToErrorList();
        }

        return result.Value;
    }
}