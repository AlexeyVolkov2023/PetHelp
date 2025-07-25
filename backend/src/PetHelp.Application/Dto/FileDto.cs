namespace PetHelp.Application.Dto;

public record CreateFileDto(Stream Content, string FileName, string ContentType);