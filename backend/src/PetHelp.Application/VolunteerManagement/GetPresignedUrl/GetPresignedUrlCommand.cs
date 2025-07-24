namespace PetHelp.Application.VolunteerManagement.GetPresignedUrl;

public record GetPresignedUrlCommand(
    string BucketName,
    string ObjectName,
    int ExpiryInSeconds = 86400);