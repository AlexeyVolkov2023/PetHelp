using FluentValidation;
using PetHelp.Application.Dto;
using PetHelp.Application.Validations;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.VolunteerManagement.UpdateSocialNetwork;

public class UpdateSocialNetworkCommandValidator :AbstractValidator<UpdateSocialNetworkCommand>
{
    public UpdateSocialNetworkCommandValidator()
    {
        RuleFor(u => u.VolunteerId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
        RuleForEach(c => c.SocialNetwork)
            .SetValidator(new SocialNetworkDtoValidator());
    }
}