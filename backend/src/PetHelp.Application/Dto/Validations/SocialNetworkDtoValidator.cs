using FluentValidation;
using PetHelp.Application.Validations;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.Dto.Validations;

public class SocialNetworkDtoValidator : AbstractValidator<SocialNetworkDto>
{
    public SocialNetworkDtoValidator()
    {
        RuleFor(c => new { c.Name, c.Link })
            .MustBeValueObject(s => PaymentDetail.Create(s.Name, s.Link));
    }
}