namespace PetHelp.Application.Dtos;

public record CreateFullNameDtos(
    string Name,
    string Surname,
    string? Patronymik);