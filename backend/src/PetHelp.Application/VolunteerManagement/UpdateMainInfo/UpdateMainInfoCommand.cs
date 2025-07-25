using PetHelp.Application.Dto;

namespace PetHelp.Application.VolunteerManagement.UpdateMainInfo;
public record UpdateMainInfoCommand(
    Guid VolunteerId,
    FullNameDto FullNameDto,
    string Email,
    string Description, 
    int ExperienceInYears,
    string PhoneNumber);
