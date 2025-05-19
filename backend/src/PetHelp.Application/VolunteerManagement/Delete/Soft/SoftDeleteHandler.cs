using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHelp.Application.Extensions;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.VolunteerManagement.Delete.Soft;

public class SoftDeleteHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IValidator<SoftDeleteCommand> _validator;
    private readonly ILogger<SoftDeleteHandler> _logger;

    public SoftDeleteHandler(
        IVolunteersRepository volunteersRepository,
        IValidator<SoftDeleteCommand> validator,
        ILogger<SoftDeleteHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
    } 
    
    public async Task<Result<Guid, ErrorList>> Handle(
        SoftDeleteCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var volunteerResult = await _volunteersRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        volunteerResult.Value.SoftDelete();
        
        var result = await _volunteersRepository.SoftDelete(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation("Deleted volunteer with Id {volunteerId}", command.VolunteerId);
        
        return result;
    }
}