using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHelp.Application.Abstraction;
using PetHelp.Application.Database;
using PetHelp.Application.Extensions;
using PetHelp.Domain.AnimalManagement.VO;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.PetManagement.Commands.UpdateSocialNetwork;

public class UpdateSocialNetworkHandler : ICommandHandler<Guid, UpdateSocialNetworkCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IValidator<UpdateSocialNetworkCommand> _validator;
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<UpdateSocialNetworkHandler> _logger;

    public UpdateSocialNetworkHandler (
        IVolunteersRepository volunteersRepository,
        IValidator<UpdateSocialNetworkCommand> validator,
        IApplicationDbContext dbContext,
        ILogger<UpdateSocialNetworkHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _dbContext = dbContext;
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

        await _dbContext.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Updated social network for volunteer with Id {volunteerId}", command.VolunteerId);
        
        return volunteerResult.Value.Id.Value;
    }
}