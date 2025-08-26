using FluentValidation;
using PetHelp.Application.Dto.Validations;

namespace PetHelp.Application.VolunteerManagement.UploadFilesToPet;

public class UploadFilesToPetCommandValidator : AbstractValidator<UploadFilesToPetCommand>
{
    public UploadFilesToPetCommandValidator()
    {
        RuleForEach(c => c.Files)
            .SetValidator(new UploadFileDtoValidator());
    }
}