using PetHelp.Application.Dto;

namespace PetHelp.Application.VolunteerManagement.UpdateSocialNetwork;

public record UpdateSocialNetworkCommand(
    Guid VolunteerId,
    IEnumerable<SocialNetworkDto> SocialNetwork);

