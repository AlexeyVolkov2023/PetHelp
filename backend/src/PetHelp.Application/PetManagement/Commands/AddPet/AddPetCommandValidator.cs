using FluentValidation;
using PetHelp.Application.Dto.Validations;
using PetHelp.Application.Validations;
using PetHelp.Domain.AnimalManagement.VO;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.PetManagement.Commands.AddPet;

public class AddPetCommandValidator : AbstractValidator<AddPetCommand>
{
    public AddPetCommandValidator()
    {
        RuleFor(a => a.VolunteerId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
        RuleFor(a => a.PetData)
            .SetValidator(new PetDataDtoValidator());
        RuleFor(a => a.PetInfo)
            .SetValidator(new PetInfoDtoValidator());
        RuleFor(a => a.Address)
            .SetValidator(new AddressDtoValidator());
        RuleFor(a => a.PhoneNumber)
            .MustBeValueObject(PhoneNumber.Create);
        RuleFor(a => a.Status)
            .MustBeValueObject(PetStatus.Create);
        RuleFor(a => a.DateOfBirth)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
        RuleFor(a => a.SpeciesId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
        RuleFor(a => a.BreedId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
        //RuleForEach(a => a.PaymentDetails)
           // .SetValidator(new PaymentDetailDtoValidator());
    }
}