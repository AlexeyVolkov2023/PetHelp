using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetHelp.Application.FileProvider;
using PetHelp.Domain.AnimalManagement.VO;
using PetHelp.Infrastructure.Providers;

namespace Pethelp.Tests;

public class MinioProviderTests : IAsyncLifetime
{
    private const string TEST_BUCKET = "test-bucket";
    private readonly IMinioClient _minioClient;
    private readonly MinioProvider _provider;
    private readonly string _testObjectPath = "test-file.txt";
    private readonly string _testContent = "Test file content";

    public MinioProviderTests()
    {
        // Подключаемся к локальному Minio серверу (должен быть запущен)
        _minioClient = new MinioClient()
            .WithEndpoint("localhost:9000")
            .WithCredentials("minioadmin", "minioadmin")
            .Build();

        var logger = new LoggerFactory().CreateLogger<MinioProvider>();
        _provider = new MinioProvider(_minioClient, logger);
    }

    public async Task InitializeAsync()
    {
        var bucketExists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(TEST_BUCKET));
        if (!bucketExists)
        {
            await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(TEST_BUCKET));
        }
        
        using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(_testContent));
        await _minioClient.PutObjectAsync(new PutObjectArgs()
            .WithBucket(TEST_BUCKET)
            .WithObject(_testObjectPath)
            .WithStreamData(stream)
            .WithObjectSize(stream.Length));
    }

    public async Task DisposeAsync()
    {
        try
        {
            await _minioClient.RemoveObjectAsync(new RemoveObjectArgs()
                .WithBucket(TEST_BUCKET)
                .WithObject(_testObjectPath));
        }
        catch
        {
            
        }
    }

    [Fact]
    public async Task UploadFiles_ShouldUploadFilesSuccessfully()
    {
        // Arrange
        var filePath = FilePath.Create(Guid.NewGuid(), ".txt").Value;
        using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(_testContent));
        var fileData = new FileData(stream, filePath, TEST_BUCKET);
        var files = new List<FileData> { fileData };

        // Act
        var result = await _provider.UploadFiles(files);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Single(result.Value);
        Assert.Equal(filePath.Path, result.Value[0].Path);

        // Проверяем, что файл действительно загружен
        var statArgs = new StatObjectArgs()
            .WithBucket(TEST_BUCKET)
            .WithObject(filePath.Path);
        
        var stat = await _minioClient.StatObjectAsync(statArgs);
        Assert.NotNull(stat);
    }
    
    [Fact]
    public async Task GetPresignedUrl_ShouldGenerateValidUrl()
    {
        // Arrange
        var filePath = FilePath.Create(Guid.NewGuid(), ".txt").Value;
        using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(_testContent));
        var fileData = new FileData(stream, filePath, TEST_BUCKET);
        
        // Сначала загружаем файл
        await _provider.UploadFiles(new List<FileData> { fileData });

        // Act
        var result = await _provider.GetPresignedUrl(TEST_BUCKET, filePath.Path, 3600);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.StartsWith($"http://localhost:9000/{TEST_BUCKET}/", result.Value);
        Assert.Contains("X-Amz-Expires=3600", result.Value);
    }
    
    [Fact]
    public async Task GetPresignedUrl_ShouldReturnError_WhenFileNotFound()
    {
        // Arrange
        var nonExistentFile = "nonexistent-" + Guid.NewGuid();

        // Act
        var result = await _provider.GetPresignedUrl(TEST_BUCKET, nonExistentFile, 3600);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("file.not_found", result.Error.Code);
    }
    
    [Fact]
    public async Task DeleteFile_ShouldRemoveFileSuccessfully()
    {
        // Arrange
        var filePath = FilePath.Create(Guid.NewGuid(), ".txt").Value;
        using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(_testContent));
        var fileData = new FileData(stream, filePath, TEST_BUCKET);
        
        // Сначала загружаем файл
        await _provider.UploadFiles(new List<FileData> { fileData });

        // Act
        var result = await _provider.RemoveFile(TEST_BUCKET, filePath.Path);

        // Assert
        Assert.True(result.IsSuccess);

        // Проверяем, что файл действительно удален
        var statArgs = new StatObjectArgs()
            .WithBucket(TEST_BUCKET)
            .WithObject(filePath.Path);

        var statFile = await _minioClient.StatObjectAsync(statArgs);
        var statBool = statFile.ContentType == null;
    }
    
    [Fact]
    public async Task DeleteFile_ShouldReturnError_WhenFileNotFound()
    {
        // Arrange
        var nonExistentFile = "nonexistent-" + Guid.NewGuid();

        // Act
        var result = await _provider.RemoveFile(TEST_BUCKET, nonExistentFile);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("file.not_found", result.Error.Code);
    }
   
}
    
    


