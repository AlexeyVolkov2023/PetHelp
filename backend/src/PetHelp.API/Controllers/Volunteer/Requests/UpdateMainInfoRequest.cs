using PetHelp.Application.Dto;

namespace PetHelp.API.Controllers.Volunteer.Requests;

public record UpdateMainInfoRequest(
    FullNameDto FullNameDto,
    string Email,
    string Description,
    int ExperienceInYears,
    string PhoneNumber);
