using FluentValidation;
using PetHelp.Application.Validations;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.PetManagement.Queries.GetVolunteersWithPaginationQuery;

public class GetFilteredVolunteersWithPaginationQueryValidation : AbstractValidator<GetFilteredVolunteersWithPaginationQuery>
{
    public GetFilteredVolunteersWithPaginationQueryValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .When(x => x.Id.HasValue)
            .WithError(Errors.General.ValueIsInvalid("Volunteer ID"));


        RuleFor(x => x.Name)
            .MaximumLength(Constants.NAME_MAX_LENGTH)
            .When(x => !string.IsNullOrEmpty(x.Name))
            .WithError(Errors.General.ValueIsInvalid("Name"));
            

        // // 3. Валидация PositionFrom
        // RuleFor(x => x.PositionFrom)
        //     .GreaterThanOrEqualTo(Constants.POSITION_MIN_VALUE)
        //     .When(x => x.PositionFrom.HasValue)
        //     .WithError(Errors.General.ValueIsOutOfRange(
        //         "PositionFrom", Constants.POSITION_MIN_VALUE, Constants.POSITION_MAX_VALUE))
        //     .LessThanOrEqualTo(Constants.POSITION_MAX_VALUE)
        //     .When(x => x.PositionFrom.HasValue)
        //     .WithError(Errors.General.ValueIsOutOfRange(
        //         "PositionFrom", Constants.POSITION_MIN_VALUE, Constants.POSITION_MAX_VALUE));
        //
        // // 4. Валидация PositionTo
        // RuleFor(x => x.PositionTo)
        //     .GreaterThanOrEqualTo(Constants.POSITION_MIN_VALUE)
        //     .When(x => x.PositionTo.HasValue)
        //     .WithError(Errors.General.ValueIsOutOfRange(
        //         "PositionTo", Constants.POSITION_MIN_VALUE, Constants.POSITION_MAX_VALUE))
        //     .LessThanOrEqualTo(Constants.POSITION_MAX_VALUE)
        //     .When(x => x.PositionTo.HasValue)
        //     .WithError(Errors.General.ValueIsOutOfRange(
        //         "PositionTo", Constants.POSITION_MIN_VALUE, Constants.POSITION_MAX_VALUE));
        //
        // // 5. Валидация диапазона позиций
        // RuleFor(x => x)
        //     .Must(x => !x.PositionFrom.HasValue || !x.PositionTo.HasValue || x.PositionFrom <= x.PositionTo)
        //     .WithName("PositionRange")
        //     .WithError(Errors.General.InvalidRange("PositionFrom", "PositionTo"));
        //
        // // 6. Валидация поля сортировки
        // RuleFor(x => x.SortBy)
        //     .Must(BeAValidSortField)
        //     .When(x => !string.IsNullOrEmpty(x.SortBy))
        //     .WithError(Errors.General.ValueIsInvalid("SortBy"))
        //     .WithMessage($"SortBy must be one of: {string.Join(", ", _allowedSortFields)}");
        //
        // // 7. Валидация направления сортировки
        // RuleFor(x => x.SortDirection)
        //     .Must(BeAValidSortDirection)
        //     .When(x => !string.IsNullOrEmpty(x.SortDirection))
        //     .WithError(Errors.General.ValueIsInvalid("SortDirection"))
        //     .WithMessage($"SortDirection must be one of: {string.Join(", ", _allowedSortDirections)}");
        //
        // // 8. Валидация номера страницы
        // RuleFor(x => x.Page)
        //     .GreaterThan(0)
        //     .WithError(Errors.General.ValueMustBeGreaterThanZero("Page"))
        //     .WithMessage("Page must be greater than 0");
        //
        // // 9. Валидация размера страницы
        // RuleFor(x => x.PageSize)
        //     .InclusiveBetween(Constants.MIN_PAGE_SIZE, Constants.MAX_PAGE_SIZE)
        //     .WithError(Errors.General.ValueIsOutOfRange(
        //         "PageSize", Constants.MIN_PAGE_SIZE, Constants.MAX_PAGE_SIZE))
        //     .WithMessage($"PageSize must be between {Constants.MIN_PAGE_SIZE} and {Constants.MAX_PAGE_SIZE}");
        //
        // // 10. Комплексная валидация - хотя бы один критерий фильтрации должен быть указан
        // RuleFor(x => x)
        //     .Must(HaveAtLeastOneFilterCriterion)
        //     .WithError(Errors.Volunteer.AtLeastOneFilterCriterionRequired())
        //     .WithMessage("At least one filter criterion must be provided (Id, Name, PositionFrom, or PositionTo)");
    }
}