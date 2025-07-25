namespace PetHelp.Application.VolunteerManagement.RemoveFile;

public record RemoveFileCommand(
    string BucketName,
    string ObjectName);