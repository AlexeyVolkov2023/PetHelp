using PetHelp.Application.Dtos;

namespace PetHelp.Application.VolunteerManagement.CreateVolunteer;

public record CreateVolunteerCommand(
    CreateFullNameDtos FullNameDto,
    string Email,
    string Description,
    int ExperienceInYears,
    string PhoneNumber,
    IEnumerable<CreatePaymentDetail> Details,
    IEnumerable<CreateSocialNetwork> Networks);