using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHelp.Application.Abstraction;
using PetHelp.Application.Database;
using PetHelp.Application.Extensions;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.PetManagement.Commands.UpdatePaymentDetail;

public class UpdatePaymentDetailHandler : ICommandHandler<Guid, UpdatePaymentDetailCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IValidator<UpdatePaymentDetailCommand> _validator;
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<UpdatePaymentDetailHandler> _logger;


    public UpdatePaymentDetailHandler(
        IVolunteersRepository volunteersRepository,
        IValidator<UpdatePaymentDetailCommand> validator,
        IApplicationDbContext dbContext,
        ILogger<UpdatePaymentDetailHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UpdatePaymentDetailCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerResult = await _volunteersRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var paymentDetails = command.PaymentDetails.Select(r => PaymentDetail.Create(r.Title, r.Description).Value);

        volunteerResult.Value.UpdatePaymentDetail(paymentDetails);

        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated requisite for volunteer with Id {volunteerId}", command.VolunteerId);

        return volunteerResult.Value.Id.Value;
    }
}