using PetHelp.Domain.AnimalManagement.VO;

namespace PetHelp.Application.FileProvider;
public record FileData(Stream Stream, FileInfo FileInfo);

public record FileInfo(FilePath FilePath, string BucketName);

