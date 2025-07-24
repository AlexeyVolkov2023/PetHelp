using PetHelp.Domain.AnimalManagement.VO;

namespace PetHelp.Application.FileProvider;
public record FileData(Stream Stream, FilePath FilePath, string BucketName);



