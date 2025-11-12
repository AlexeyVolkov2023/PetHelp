using FluentValidation;
using PetHelp.Application.Validations;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.SpeciesManagement.Commands.DeleteSpeciesBreed;

public class DeleteSpeciesBreedCommandValidator : AbstractValidator<DeleteSpeciesBreedCommand>
{
    public DeleteSpeciesBreedCommandValidator()
    {
        RuleFor(x => x.SpeciesId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());

        RuleFor(x => x.BreedId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
    }
} 
