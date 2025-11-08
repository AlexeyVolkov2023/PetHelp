using PetHelp.Application.Abstraction;

namespace PetHelp.Application.PetManagement.Commands.RemoveFile;

public record RemoveFileCommand(
    string BucketName,
    string ObjectName) : ICommand;