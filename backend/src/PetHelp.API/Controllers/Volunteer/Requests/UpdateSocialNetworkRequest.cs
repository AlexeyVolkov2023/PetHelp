using PetHelp.Application.Dtos;

namespace PetHelp.API.Controllers.Volunteer.Requests;

public record UpdateSocialNetworkRequest(IEnumerable<SocialNetworkDto> SocialNetwork);