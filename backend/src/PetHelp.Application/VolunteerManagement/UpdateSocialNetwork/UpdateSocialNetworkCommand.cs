using PetHelp.Application.Dtos;

namespace PetHelp.Application.VolunteerManagement.UpdateSocialNetwork;

public record UpdateSocialNetworkCommand(
    Guid VolunteerId,
    IEnumerable<SocialNetworkDto> SocialNetwork);

