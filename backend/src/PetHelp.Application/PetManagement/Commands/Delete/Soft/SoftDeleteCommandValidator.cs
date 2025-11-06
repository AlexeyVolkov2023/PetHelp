using FluentValidation;
using PetHelp.Application.Validations;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.PetManagement.Commands.Delete.Soft;

public class SoftDeleteCommandValidator : AbstractValidator<SoftDeleteCommand>
{
    public SoftDeleteCommandValidator()
    {
        RuleFor(s => s.VolunteerId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
    }
}