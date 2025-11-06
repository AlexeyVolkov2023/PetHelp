namespace PetHelp.Application.Dto;

public class PetDataDto
{
    public string Color { get; init; } = String.Empty;
    public string HealthInfo { get; init; } = String.Empty;
    public double Weight { get; init; }
    public double Height { get; init; }
    public bool IsNeutered { get; init; }
    public bool IsVaccinated{ get; init; }
}