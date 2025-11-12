using FluentValidation;
using PetHelp.Application.Validations;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.SpeciesManagement.Queries.GetBreedsBySpeciesId;

public class GetBreedsBySpeciesIdQueryValidator : AbstractValidator<GetBreedsBySpeciesIdQuery>
{
    public GetBreedsBySpeciesIdQueryValidator()
    {
        RuleFor(x => x.SpeciesId)
            .NotEmpty().When(x => x.SpeciesId.HasValue)
            .WithError(Errors.General.ValueIsRequired());

        RuleFor(x => x.Title)
            .MaximumLength(Constants.TITLE_MAX_LENGTH).When(x => !string.IsNullOrEmpty(x.Title))
            .WithError(Errors.General.ValueIsRequired());

        RuleFor(x => x.PositionTo)
            .GreaterThanOrEqualTo(0).When(x => x.PositionTo.HasValue)
            .WithError(Errors.General.ValueIsRequired());

        RuleFor(x => x.PositionFrom)
            .GreaterThanOrEqualTo(0).When(x => x.PositionFrom.HasValue)
            .WithError(Errors.General.ValueIsRequired());

        RuleFor(x => x.SortBy)
            .Must(BeAValidSortProperty).When(x => !string.IsNullOrEmpty(x.SortBy))
            .WithError(Errors.General.ValueIsRequired());

        RuleFor(x => x.SortDirection)
            .Must(BeAValidSortDirection).When(x => !string.IsNullOrEmpty(x.SortDirection))
            .WithError(Errors.General.ValueIsRequired());

        RuleFor(x => x.Page)
            .GreaterThan(0)
            .WithError(Errors.General.ValueIsRequired());

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100)
            .WithError(Errors.General.ValueIsRequired());
    }
    private bool BeAValidSortProperty(string sortBy)
    {
        var validProperties = new[] { "title" }; 
        return validProperties.Contains(sortBy, StringComparer.OrdinalIgnoreCase);
    }

    private bool BeAValidSortDirection(string sortDirection)
    {
        return sortDirection.Equals("asc", StringComparison.OrdinalIgnoreCase) ||
               sortDirection.Equals("desc", StringComparison.OrdinalIgnoreCase);
    }
    
    
}