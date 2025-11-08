using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PetHelp.Application.Files;


namespace PetHelp.Infrastructure.BackgroundServices;

public class FilesCleanerBackgroundServices : BackgroundService
{
    private readonly ILogger<FilesCleanerBackgroundServices> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public FilesCleanerBackgroundServices(
        ILogger<FilesCleanerBackgroundServices> logger,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("FilesCLeanerBackgroundServices is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            await using var scope = _scopeFactory.CreateAsyncScope();

            var filesCleanerService = scope.ServiceProvider.GetRequiredService<IFilesCleanerService>();

            await filesCleanerService.Process(stoppingToken);
        }

        await Task.CompletedTask;
    }
}