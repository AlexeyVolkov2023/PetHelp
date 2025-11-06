namespace PetHelp.Application.Dto;

public class VolunteerDto
{
    public Guid Id { get; init; }
    public FullNameDto FullName { get; init; }
    public string Email { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public int ExperienceInYears { get; init; }
    public string PhoneNumber { get; init; } = string.Empty;
    public SocialNetworkDto[] SocialNetworks { get; init; } = [];
    public PaymentDetailDto[] PaymentDetails { get; init; } = [];
}