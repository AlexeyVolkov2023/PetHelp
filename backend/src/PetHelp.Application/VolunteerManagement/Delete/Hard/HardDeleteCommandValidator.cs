using FluentValidation;
using PetHelp.Application.Validations;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.VolunteerManagement.Delete.Hard;

public class HardDeleteRequestValidator : AbstractValidator<HardDeleteCommand>
{
    public HardDeleteRequestValidator()
    {
        RuleFor(h => h.VolunteerId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
    }
}