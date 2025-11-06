using PetHelp.Application.Abstraction;

namespace PetHelp.Application.PetManagement.Commands.GetPresignedUrl;

public record GetPresignedUrlCommand(
    string BucketName,
    string ObjectName,
    int ExpiryInSeconds = 86400) : ICommand;