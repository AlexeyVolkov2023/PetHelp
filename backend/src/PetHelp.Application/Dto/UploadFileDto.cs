namespace PetHelp.Application.Dto;

public record UploadFileDto(Stream Content, string FileName, string ContentType);