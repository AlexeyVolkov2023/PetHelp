using CSharpFunctionalExtensions;
using PetHelp.Domain.AnimalManagement.VO;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.Files;

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
    
    Task<UnitResult<Error>> RemoveFile(
        string bucketName,
        string objectPath,
        CancellationToken cancellationToken = default);
    
    Task<UnitResult<Error>> RemoveFile(
        FileInfo fileInfo,
        CancellationToken cancellationToken = default);
   
}