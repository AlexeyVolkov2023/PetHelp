using PetHelp.Domain.AnimalManagement.VO;

namespace PetHelp.Application.Files;
public record FileData(Stream Stream, FileInfo FileInfo);

public record FileInfo(FilePath FilePath, string BucketName);

