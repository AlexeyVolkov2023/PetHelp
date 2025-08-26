using FluentValidation;
using PetHelp.Application.Validations;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.Dto.Validations;

public class PaymentDetailDtoValidator :AbstractValidator<PaymentDetailDto>
{
    public PaymentDetailDtoValidator()
    {
        RuleFor(c => new{c.Title, c.Description})
            .MustBeValueObject(s => PaymentDetail.Create(s.Title, s.Description));
    }
}