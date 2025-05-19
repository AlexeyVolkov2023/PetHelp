using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHelp.Application.Extensions;
using PetHelp.Domain.AnimalManagement.VO;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.VolunteerManagement.UpdateSocialNetwork;

public class UpdateSocialNetworkHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IValidator<UpdateSocialNetworkCommand> _validator;
    private readonly ILogger<UpdateSocialNetworkHandler> _logger;

    public UpdateSocialNetworkHandler (
        IVolunteersRepository volunteersRepository,
        IValidator<UpdateSocialNetworkCommand> validator,
        ILogger<UpdateSocialNetworkHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
    }
    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateSocialNetworkCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerResult = await _volunteersRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure) 
            return volunteerResult.Error.ToErrorList();

        var socialNetwork = command.SocialNetwork
            .Select(r => SocialNetwork.Create(r.Name, r.Link).Value);
        
        volunteerResult.Value.UpdateSocialNetworks(socialNetwork);

        var result = await _volunteersRepository.Save(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation("Updated social network for volunteer with Id {volunteerId}", command.VolunteerId);
        
        return result;
    }
}