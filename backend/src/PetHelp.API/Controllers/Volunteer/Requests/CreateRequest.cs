using PetHelp.Application.Dtos;
using PetHelp.Application.VolunteerManagement.Create;

namespace PetHelp.API.Controllers.Volunteer.Requests;

public record CreateRequest(
    FullNameDto FullNameDto,
    string Email,
    string Description,
    int ExperienceInYears,
    string PhoneNumber,
    IEnumerable<PaymentDetailDto> PaymentDetails,
    IEnumerable<SocialNetworkDto> SocialNetworks)
{
    public CreateCommand ToCommand() => new(
        FullNameDto,
        Email,
        Description,
        ExperienceInYears,
        PhoneNumber,
        PaymentDetails,
        SocialNetworks);
}