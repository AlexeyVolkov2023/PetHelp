using PetHelp.Application.Abstraction;
using PetHelp.Application.Dto;

namespace PetHelp.Application.PetManagement.Commands.Create;
    
public record CreateCommand(
    FullNameDto FullNameDto,
    string Email,
    string Description,
    int ExperienceInYears,
    string PhoneNumber,
    IEnumerable<PaymentDetailDto> PaymentDetails,
    IEnumerable<SocialNetworkDto> SocialNetworks) : ICommand;