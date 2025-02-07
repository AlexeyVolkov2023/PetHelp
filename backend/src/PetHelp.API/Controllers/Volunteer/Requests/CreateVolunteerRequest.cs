using PetHelp.Application.Dtos;

namespace PetHelp.API.Controllers.Volunteer.Requests;

public record CreateVolunteerRequest(
    string Name,
    string Surname,
    string? Patronymik,
    string Email,
    string Description,
    int ExperienceInYears,
    string PhoneNumber,
    IEnumerable<CreatePaymentDetail> Details,
    IEnumerable<CreateSocialNetwork> Networks);