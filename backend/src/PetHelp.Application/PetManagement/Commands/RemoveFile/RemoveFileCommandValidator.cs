using FluentValidation;
using PetHelp.Application.Validations;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.PetManagement.Commands.RemoveFile;

public class RemoveFileCommandValidator : AbstractValidator<RemoveFileCommand>
{
    public RemoveFileCommandValidator()
    {
        RuleFor(s => s.BucketName)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
        RuleFor(s => s.ObjectName)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
    }
}