using PetHelp.Application.Dtos;

namespace PetHelp.Application.VolunteerManagement.CreateVolunteer;

public record CreateVolunteerCommand(
    string Name,
    string Surname,
    string? Patronymik,
    string Email,
    string Description,
    int ExperienceInYears,
    string PhoneNumber,
    IEnumerable<CreatePaymentDetail> Details,
    IEnumerable<CreateSocialNetwork> Networks);

