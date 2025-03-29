using FluentValidation;
using PetHelp.Application.Validations;
using PetHelp.Domain.AnimalManagement.VO;

namespace PetHelp.Application.Dtos;

public class CreateFullNameDtosValidator : AbstractValidator<CreateFullNameDtos>
{
    public CreateFullNameDtosValidator()
    {
        RuleFor(c => new { c.Name, c.Surname, c.Patronymik })
            .MustBeValueObject(x =>
                FullName.Create(x.Name, x.Surname, x.Patronymik!));
    }
}