using FluentValidation;

namespace PetHelp.Application.Dto.Validations;

public class UploadFileDtoValidator : AbstractValidator<UploadFileDto>
{
    public UploadFileDtoValidator()
    {
        RuleFor(d => d.Content)
            .Must(u => u.Length < 5000000);
    }
}