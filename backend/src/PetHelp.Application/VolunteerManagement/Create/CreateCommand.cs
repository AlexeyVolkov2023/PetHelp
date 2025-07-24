using PetHelp.Application.Dto;

namespace PetHelp.Application.VolunteerManagement.Create;
    
public record CreateCommand(
    FullNameDto FullNameDto,
    string Email,
    string Description,
    int ExperienceInYears,
    string PhoneNumber,
    IEnumerable<PaymentDetailDto> PaymentDetails,
    IEnumerable<SocialNetworkDto> SocialNetworks);