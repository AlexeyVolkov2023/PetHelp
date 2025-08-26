using FluentValidation;
using PetHelp.Application.Dto;
using PetHelp.Application.Dto.Validations;
using PetHelp.Application.Validations;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.VolunteerManagement.UpdatePaymentDetail;

public class UpdatePaymentDetailCommandValidator :AbstractValidator<UpdatePaymentDetailCommand>
{
    public UpdatePaymentDetailCommandValidator()
    {
        RuleFor(u => u.VolunteerId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
        RuleForEach(c => c.PaymentDetails)
            .SetValidator(new PaymentDetailDtoValidator());
    }
}