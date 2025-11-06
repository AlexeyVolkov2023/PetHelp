using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel;
using Minio.DataModel.Args;
using Minio.Exceptions;
using PetHelp.Application.Files;
using PetHelp.Domain.AnimalManagement.VO;
using PetHelp.Domain.Shared;
using FileInfo = PetHelp.Application.Files.FileInfo;

namespace PetHelp.Infrastructure.Providers;

public class MinioProvider : IFileProvider
{
    private const int MAX_DEGREE_OF_PARALELLISM = 10;
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;

    public MinioProvider(
        IMinioClient minioClient,
        ILogger<MinioProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }

    public async Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(
        IEnumerable<FileData> filesData,
        CancellationToken cancellationToken = default)
    {
        var semaphoreSlim = new SemaphoreSlim(MAX_DEGREE_OF_PARALELLISM);
        var filesList = filesData.ToList();

        try
        {
            await IfBucketsNotExistCreateBucket(filesList.Select(file => file.FileInfo.BucketName), cancellationToken);

            var tasks = filesList.Select(async file =>
                await PutObject(file, semaphoreSlim, cancellationToken));

            var pathsResult = await Task.WhenAll(tasks);
            if (pathsResult.Any(p => p.IsFailure))
                return pathsResult.First().Error;

            var results = pathsResult.Select(p => p.Value).ToList();

            _logger.LogInformation("Uploaded files {files}", results.Select(f => f.Path));

            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to upload files in minio, files amount: {amount}", filesList.Count);

            return Error.Failure("file.upload", "Fail to upload files in minio");
        }
    }

    private async Task<Result<FilePath, Error>> PutObject(
        FileData fileData,
        SemaphoreSlim semaphoreSlim,
        CancellationToken cancellationToken)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(fileData.FileInfo.BucketName)
            .WithStreamData(fileData.Stream)
            .WithObjectSize(fileData.Stream.Length)
            .WithObject(fileData.FileInfo.FilePath.Path);

        try
        {
            await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

            return fileData.FileInfo.FilePath;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Fail to upload file in minio with path {path} in bucket {bucket}",
                fileData.FileInfo.FilePath.Path,
                fileData.FileInfo.BucketName);

            return Error.Failure("file.upload", "Fail to upload file in minio");
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    private async Task IfBucketsNotExistCreateBucket(
        IEnumerable<string> buckets,
        CancellationToken cancellationToken)
    {
        HashSet<string> bucketNames = [..buckets];

        foreach (var bucketName in bucketNames)
        {
            var bucketExistArgs = new BucketExistsArgs()
                .WithBucket(bucketName);

            var bucketExist = await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);
            if (bucketExist == false)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);

                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }
        }
    }

    public async Task<Result<string, Error>> GetPresignedUrl(
        string bucketName,
        string objectName,
        int expiryInSeconds,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var bucketExistsArgs = new BucketExistsArgs()
                .WithBucket(bucketName);

            var bucketExists = await _minioClient.BucketExistsAsync(bucketExistsArgs, cancellationToken);
            if (!bucketExists)
                return Error.Failure("file.not_found", "Bucket does not exist");

            var statArgs = new StatObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName);

            var statFile = await _minioClient.StatObjectAsync(statArgs, cancellationToken);
            if (statFile.ContentType == null)
            {
                _logger.LogError(
                    "File with bucket \"{BucketName}\", and name \"{FileName}\" not found",
                    bucketName,
                    objectName);
                return Errors.FileProvider.NotFound(objectName);
            }

            var args = new PresignedGetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithExpiry(expiryInSeconds);

            var url = await _minioClient.PresignedGetObjectAsync(args);

            return url;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex, "Failed to generate presigned URL for {ObjectName} in bucket {BucketName}",
                objectName,
                bucketName);

            return Error.Failure("file.url_generation_failed", "Failed to generate presigned URL");
        }
    }


    public async Task<UnitResult<Error>> RemoveFile(
        string bucketName,
        string objectName,
        CancellationToken cancellationToken = default)
    {
        return await RemoveFileInternal(bucketName, objectName, cancellationToken);
    }

    public async Task<UnitResult<Error>> RemoveFile(
        FileInfo fileInfo,
        CancellationToken cancellationToken = default)
    {
        return await RemoveFileInternal(fileInfo.BucketName, fileInfo.FilePath.Path, cancellationToken);
    }

    private async Task<UnitResult<Error>> RemoveFileInternal(
        string bucketName,
        string objectName,
        CancellationToken cancellationToken)
    {
        try
        {
            var bucketExistsArgs = new BucketExistsArgs()
                .WithBucket(bucketName);

            var bucketExists = await _minioClient.BucketExistsAsync(bucketExistsArgs, cancellationToken);
            if (!bucketExists)
            {
                _logger.LogInformation("Bucket {BucketName} does not exist", bucketName);
                return Result.Success<Error>();
            }

            var statArgs = new StatObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName);

            var statFile = await _minioClient.StatObjectAsync(statArgs, cancellationToken);
            if (statFile.ContentType == null)
            {
                _logger.LogError(
                    "File with bucket \"{BucketName}\", and name \"{FileName}\" not found",
                    bucketName,
                    objectName);
                return Errors.FileProvider.NotFound(objectName);
            }

            var removeArgs = new RemoveObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName);

            await _minioClient.RemoveObjectAsync(removeArgs, cancellationToken);

            _logger.LogInformation("Successfully deleted file {ObjectName} from bucket {BucketName}",
                objectName, bucketName);

            return Result.Success<Error>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Failed to delete file {ObjectName} from bucket {BucketName}",
                objectName,
                bucketName);

            return Error.Failure("file.delete_failed", "Failed to delete file from storage");
        }
    }
}