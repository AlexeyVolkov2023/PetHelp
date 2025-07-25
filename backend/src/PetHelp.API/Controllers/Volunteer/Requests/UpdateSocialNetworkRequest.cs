using PetHelp.Application.Dto;

namespace PetHelp.API.Controllers.Volunteer.Requests;

public record UpdateSocialNetworkRequest(IEnumerable<SocialNetworkDto> SocialNetwork);