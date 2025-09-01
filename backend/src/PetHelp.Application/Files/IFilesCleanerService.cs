namespace PetHelp.Application.Files;

public interface IFilesCleanerService
{
    Task Process(CancellationToken cancellationToken);
}