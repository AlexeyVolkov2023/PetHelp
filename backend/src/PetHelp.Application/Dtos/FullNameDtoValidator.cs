using FluentValidation;
using PetHelp.Application.Validations;
using PetHelp.Domain.AnimalManagement.VO;

namespace PetHelp.Application.Dtos;

public class FullNameDtoValidator : AbstractValidator<FullNameDto>
{
    public FullNameDtoValidator()
    {
        RuleFor(c => new { c.Name, c.Surname, c.Patronymic })
            .MustBeValueObject(x =>
                FullName.Create(x.Name, x.Surname, x.Patronymic!));
    }
}