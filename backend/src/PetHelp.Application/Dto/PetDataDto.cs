namespace PetHelp.Application.Dto;

public record PetDataDto(
    string Color,
    string HealthInfo,
    double Weight,
    double Height,
    bool IsNeutered,
    bool IsVaccinated);