using PetHelp.Application.Dtos;
using PetHelp.Application.VolunteerManagement.CreateVolunteer;

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
    IEnumerable<CreateSocialNetwork> Networks)
{
    public CreateVolunteerCommand ToCommand() =>
        new (
            new CreateFullNameDtos(Name, Surname, Patronymik),
            Email,
            Description,
            ExperienceInYears,
            PhoneNumber,
            Details,
            Networks);
} 