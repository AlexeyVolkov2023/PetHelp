using FluentValidation;
using PetHelp.Application.Validations;
using PetHelp.Domain.AnimalManagement.VO;

namespace PetHelp.Application.Dto.Validations;

public class AddressDtoValidator: AbstractValidator<AddressDto>
{
    public AddressDtoValidator()
    {
        RuleFor(c => new { c.Country, c.Region, c.City, c.Street, c.HouseNumber, c.Apartment})
            .MustBeValueObject(x =>
                Address.Create(x.Country, x.Region, x.City, x.Street, x.HouseNumber, x.Apartment));
    }
}