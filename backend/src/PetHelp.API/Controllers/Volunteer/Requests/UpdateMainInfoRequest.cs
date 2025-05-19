using PetHelp.Application.Dtos;
using PetHelp.Application.VolunteerManagement.UpdateMainInfo;

namespace PetHelp.API.Controllers.Volunteer.Requests;

public record UpdateMainInfoRequest(
    FullNameDto FullNameDto,
    string Email,
    string Description,
    int ExperienceInYears,
    string PhoneNumber);
