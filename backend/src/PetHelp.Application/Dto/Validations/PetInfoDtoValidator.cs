using FluentValidation;
using PetHelp.Application.Validations;
using PetHelp.Domain.AnimalManagement.VO;

namespace PetHelp.Application.Dto.Validations;

public class PetInfoDtoValidator : AbstractValidator<PetInfoDto>
{
    public PetInfoDtoValidator()
    {
        RuleFor(c => new { c.Name, c.Description })
            .MustBeValueObject(x =>
                PetInfo.Create(x.Name, x.Description));
    }
}