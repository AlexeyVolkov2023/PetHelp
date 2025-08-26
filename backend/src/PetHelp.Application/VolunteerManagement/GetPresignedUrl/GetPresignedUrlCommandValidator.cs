using FluentValidation;
using PetHelp.Application.Validations;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.VolunteerManagement.GetPresignedUrl;

public class GetPresignedUrlCommandValidator : AbstractValidator<GetPresignedUrlCommand>
{
    public GetPresignedUrlCommandValidator()
    {
        RuleFor(s => s.BucketName)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
        RuleFor(s => s.ObjectName)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
    }
}