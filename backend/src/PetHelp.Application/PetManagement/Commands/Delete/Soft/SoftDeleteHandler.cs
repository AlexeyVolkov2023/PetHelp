using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHelp.Application.Abstraction;
using PetHelp.Application.Database;
using PetHelp.Application.Extensions;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.PetManagement.Commands.Delete.Soft;

public class SoftDeleteHandler : ICommandHandler<Guid, SoftDeleteCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IValidator<SoftDeleteCommand> _validator;
    private readonly ILogger<SoftDeleteHandler> _logger;
    private readonly IApplicationDbContext _dbContext;

    public SoftDeleteHandler(
        IVolunteersRepository volunteersRepository,
        IValidator<SoftDeleteCommand> validator,
        ILogger<SoftDeleteHandler> logger,
        IApplicationDbContext dbContext)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
        _dbContext = dbContext;
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
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Deleted volunteer with Id {volunteerId}", command.VolunteerId);
        
        return volunteerResult.Value.Id.Value;
    }
}