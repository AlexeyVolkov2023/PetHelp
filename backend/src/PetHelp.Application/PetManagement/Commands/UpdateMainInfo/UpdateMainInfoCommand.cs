using PetHelp.Application.Abstraction;
using PetHelp.Application.Dto;

namespace PetHelp.Application.PetManagement.Commands.UpdateMainInfo;
public record UpdateMainInfoCommand(
    Guid VolunteerId,
    FullNameDto FullNameDto,
    string Email,
    string Description, 
    int ExperienceInYears,
    string PhoneNumber) : ICommand;
