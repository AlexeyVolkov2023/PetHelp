using FluentValidation;
using PetHelp.Application.Dtos;
using PetHelp.Application.Validations;
using PetHelp.Domain.AnimalManagement.VO;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.VolunteerManagement.CreateVolunteer;

public class CreateVolunteerCommandValidator : AbstractValidator<CreateVolunteerCommand>
{
    public CreateVolunteerCommandValidator()
    {
        RuleFor(c => c.FullNameDto)
            .SetValidator(new CreateFullNameDtosValidator());
        RuleFor(c => c.Email)
            .MustBeValueObject(Email.Create);
        RuleFor(c => c.Description)
            .MustBeValueObject(Description.Create);
        RuleFor(c => c.ExperienceInYears)
            .MustBeValueObject(ExperienceInYears.Create);
        RuleFor(c => c.PhoneNumber)
            .MustBeValueObject(PhoneNumber.Create);
        RuleForEach(c => c.Details)
            .MustBeValueObject(s => PaymentDetail.Create(s.Title, s.Description));
        RuleForEach(c => c.Networks)
            .MustBeValueObject(s => SocialNetwork.Create(s.Name, s.Link));
    }
}