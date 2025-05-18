using FluentValidation;
using PetHelp.Application.Validations;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.Dtos;

public class SocialNetworkDtoValidator : AbstractValidator<SocialNetworkDto>
{
    public SocialNetworkDtoValidator()
    {
        RuleFor(c => new { c.Name, c.Link })
            .MustBeValueObject(s => PaymentDetail.Create(s.Name, s.Link));
    }
}