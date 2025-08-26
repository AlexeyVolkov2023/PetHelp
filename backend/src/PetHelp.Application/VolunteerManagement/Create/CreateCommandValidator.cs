using FluentValidation;
using PetHelp.Application.Dto;
using PetHelp.Application.Dto.Validations;
using PetHelp.Application.Validations;
using PetHelp.Domain.AnimalManagement.VO;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.VolunteerManagement.Create;

public class CreateCommandValidator : AbstractValidator<CreateCommand>
{
    public CreateCommandValidator()
    {
        RuleFor(c => c.FullNameDto)
            .SetValidator(new FullNameDtoValidator());
        RuleFor(c => c.Email)
            .MustBeValueObject(Email.Create);
        RuleFor(c => c.Description)
            .MustBeValueObject(Description.Create);
        RuleFor(c => c.ExperienceInYears)
            .MustBeValueObject(ExperienceInYears.Create);
        RuleFor(c => c.PhoneNumber)
            .MustBeValueObject(PhoneNumber.Create);
        RuleForEach(c => c.PaymentDetails)
            .SetValidator(new PaymentDetailDtoValidator());
        RuleForEach(c => c.SocialNetworks)
            .SetValidator(new SocialNetworkDtoValidator());
    }
}


