using CSharpFunctionalExtensions;
using PetHelp.Application.FileProvider;
using PetHelp.Domain.AnimalManagement.VO;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.Providers;

public interface IFileProvider
{
    Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(
        IEnumerable<FileData> filesData,
        CancellationToken cancellationToken = default);
    
    Task<Result<string, Error>> GetPresignedUrl(
        string bucketName,
        string objectName,
        int expiryInSeconds,
        CancellationToken cancellationToken = default);
    
    Task<Result<string, Error>> RemoveFile(
        string bucketName,
        string objectPath,
        CancellationToken cancellationToken = default);
}