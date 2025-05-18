using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHelp.Application.Extensions;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.VolunteerManagement.UpdatePaymentDetail;

public class UpdatePaymentDetailHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IValidator<UpdatePaymentDetailCommand> _validator;
    private readonly ILogger<UpdatePaymentDetailHandler> _logger;

    public UpdatePaymentDetailHandler (
        IVolunteersRepository volunteersRepository,
        IValidator<UpdatePaymentDetailCommand> validator,
        ILogger<UpdatePaymentDetailHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
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

        var paymentDetails = command.PaymentDetails.
            Select(r => PaymentDetail.Create(r.Title, r.Description).Value);
        
        volunteerResult.Value.UpdatePaymentDetail(paymentDetails);

        var result = await _volunteersRepository.Save(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation("Updated requisite for volunteer with Id {volunteerId}", command.VolunteerId);
        
        return result;
    }
}