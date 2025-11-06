using PetHelp.Application.Abstraction;
using PetHelp.Application.Dto;

namespace PetHelp.Application.PetManagement.Commands.UpdateSocialNetwork;

public record UpdateSocialNetworkCommand(
    Guid VolunteerId,
    IEnumerable<SocialNetworkDto> SocialNetwork) : ICommand;

