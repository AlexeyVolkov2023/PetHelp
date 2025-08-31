using Microsoft.Extensions.Logging;
using PetHelp.Application.FileProvider;
using PetHelp.Application.Files;
using PetHelp.Application.Messaging;
using FileInfo = PetHelp.Application.FileProvider.FileInfo;

namespace PetHelp.Infrastructure.BackgroundServices;

public class FilesCleanerService : IFilesCleanerService
{
    private readonly ILogger<FilesCleanerBackgroundServices> _logger;
    private readonly IMessageQueue<IEnumerable<FileInfo>> _messageQueue;
    private readonly IFileProvider _fileProvider;

    public FilesCleanerService(
        ILogger<FilesCleanerBackgroundServices> logger, 
        IMessageQueue<IEnumerable<FileInfo>> messageQueue,
        IFileProvider fileProvider)
    {
        _logger = logger;
        _messageQueue = messageQueue;
        _fileProvider = fileProvider;
    }

    public async Task Process(CancellationToken cancellationToken)
    {
        var filesInfo = await _messageQueue.ReadAsync(cancellationToken);
            
        foreach (var fileInfo in filesInfo)
        {
            await _fileProvider.RemoveFile(fileInfo, cancellationToken);
        } 
    }
}