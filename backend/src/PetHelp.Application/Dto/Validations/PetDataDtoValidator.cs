using FluentValidation;
using PetHelp.Application.Validations;
using PetHelp.Domain.AnimalManagement.VO;

namespace PetHelp.Application.Dto.Validations;

public class PetDataDtoValidator : AbstractValidator<PetDataDto>
{
    public PetDataDtoValidator()
    {
        RuleFor(c => new { c.Color, c.HealthInfo, c.Height, c.Weight, c.IsNeutered, c.IsVaccinated })
            .MustBeValueObject(x =>
                PetData.Create( x.Color, x.HealthInfo, x.Height, x.Weight, x.IsNeutered, x.IsVaccinated));
    }
}