using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHelp.Application.Database;
using PetHelp.Application.Extensions;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.VolunteerManagement.Delete.Hard;

public class HardDeleteHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IValidator<HardDeleteCommand> _validator;
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<HardDeleteHandler> _logger;
   

    public HardDeleteHandler(
        IVolunteersRepository volunteersRepository,
        IValidator<HardDeleteCommand> validator,
        IApplicationDbContext dbContext,
        ILogger<HardDeleteHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        HardDeleteCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerResult = await _volunteersRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Hard deleted volunteer with Id {volunteerId}", command.VolunteerId);

        return volunteerResult.Value.Id.Value;
    }
}