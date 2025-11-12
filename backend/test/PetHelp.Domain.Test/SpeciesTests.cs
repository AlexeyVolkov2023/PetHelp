using FluentAssertions;
using FluentValidation.TestHelper;
using PetHelp.Application.PetManagement.Queries.GetVolunteersWithPaginationQuery;
using PetHelp.Domain.Shared;


namespace PetHelp.Test;

public class SpeciesTests
{
    private readonly GetFilteredVolunteersWithPaginationQueryValidation _validator;

    public SpeciesTests()
    {
        _validator = new GetFilteredVolunteersWithPaginationQueryValidation();
    }

    // ==================== ID ВАЛИДАЦИЯ ====================

    [Fact]
    public void Validate_WhenIdIsNull_ShouldNotHaveValidationError()
    {
        // Arrange
        var query = new GetFilteredVolunteersWithPaginationQuery(
            Id: null,
            Name: "John Doe",
            PositionFrom: 0,
            PositionTo: 100,
            SortBy: "name",
            SortDirection: "asc",
            Page: 1,
            PageSize: 10);

        // Act & Assert
        _validator.TestValidate(query).ShouldNotHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Validate_WhenIdIsValidGuid_ShouldNotHaveValidationError()
    {
        // Arrange
        var query = new GetFilteredVolunteersWithPaginationQuery(
            Id: Guid.NewGuid(),
            Name: "John Doe",
            PositionFrom: 0,
            PositionTo: 100,
            SortBy: "name",
            SortDirection: "asc",
            Page: 1,
            PageSize: 10);

        // Act & Assert
        _validator.TestValidate(query).ShouldNotHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Validate_WhenIdIsEmptyGuid_ShouldHaveValidationError()
    {
        // Arrange
        var query = new GetFilteredVolunteersWithPaginationQuery(
            Id: Guid.Empty,
            Name: "John Doe",
            PositionFrom: 0,
            PositionTo: 100,
            SortBy: "name",
            SortDirection: "asc",
            Page: 1,
            PageSize: 10);

        // Act & Assert
        _validator.TestValidate(query).ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorCode("value.is.invalid")
            .WithErrorMessage("Volunteer ID is invalid");
    }

    // ==================== NAME ВАЛИДАЦИЯ ====================

    [Fact]
    public void Validate_WhenNameIsNull_ShouldNotHaveValidationError()
    {
        // Arrange
        var query = new GetFilteredVolunteersWithPaginationQuery(
            Id: null,
            Name: null,
            PositionFrom: 0,
            PositionTo: 100,
            SortBy: "name",
            SortDirection: "asc",
            Page: 1,
            PageSize: 10);

        // Act & Assert
        _validator.TestValidate(query).ShouldNotHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Validate_WhenNameIsEmpty_ShouldNotHaveValidationError()
    {
        // Arrange
        var query = new GetFilteredVolunteersWithPaginationQuery(
            Id: null,
            Name: "",
            PositionFrom: 0,
            PositionTo: 100,
            SortBy: "name",
            SortDirection: "asc",
            Page: 1,
            PageSize: 10);

        // Act & Assert
        _validator.TestValidate(query).ShouldNotHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Validate_WhenNameIsValid_ShouldNotHaveValidationError()
    {
        // Arrange
        var query = new GetFilteredVolunteersWithPaginationQuery(
            Id: null,
            Name: "John Doe",
            PositionFrom: 0,
            PositionTo: 100,
            SortBy: "name",
            SortDirection: "asc",
            Page: 1,
            PageSize: 10);

        // Act & Assert
        _validator.TestValidate(query).ShouldNotHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Validate_WhenNameExceedsMaxLength_ShouldHaveValidationError()
    {
        // Arrange
        var longName = new string('A', Constants.NAME_MAX_LENGTH + 1);
        var query = new GetFilteredVolunteersWithPaginationQuery(
            Id: null,
            Name: longName,
            PositionFrom: 0,
            PositionTo: 100,
            SortBy: "name",
            SortDirection: "asc",
            Page: 1,
            PageSize: 10);

        // Act & Assert
        _validator.TestValidate(query).ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorCode("value.is.invalid")
            .WithErrorMessage("Name is invalid");
    }

    [Fact]
    public void Validate_WhenNameIsAtMaxLength_ShouldNotHaveValidationError()
    {
        // Arrange
        var maxLengthName = new string('A', Constants.NAME_MAX_LENGTH);
        var query = new GetFilteredVolunteersWithPaginationQuery(
            Id: null,
            Name: maxLengthName,
            PositionFrom: 0,
            PositionTo: 100,
            SortBy: "name",
            SortDirection: "asc",
            Page: 1,
            PageSize: 10);

        // Act & Assert
        _validator.TestValidate(query).ShouldNotHaveValidationErrorFor(x => x.Name);
    }

    // ==================== POSITION ВАЛИДАЦИЯ ====================

    [Fact]
    public void Validate_WhenPositionFromIsNull_ShouldNotHaveValidationError()
    {
        // Arrange
        var query = new GetFilteredVolunteersWithPaginationQuery(
            Id: null,
            Name: "John",
            PositionFrom: null,
            PositionTo: 100,
            SortBy: "name",
            SortDirection: "asc",
            Page: 1,
            PageSize: 10);

        // Act & Assert
        _validator.TestValidate(query).ShouldNotHaveValidationErrorFor(x => x.PositionFrom);
    }

    [Fact]
    public void Validate_WhenPositionToIsNull_ShouldNotHaveValidationError()
    {
        // Arrange
        var query = new GetFilteredVolunteersWithPaginationQuery(
            Id: null,
            Name: "John",
            PositionFrom: 0,
            PositionTo: null,
            SortBy: "name",
            SortDirection: "asc",
            Page: 1,
            PageSize: 10);

        // Act & Assert
        _validator.TestValidate(query).ShouldNotHaveValidationErrorFor(x => x.PositionTo);
    }

    [Fact]
    public void Validate_WhenPositionFromIsValid_ShouldNotHaveValidationError()
    {
        // Arrange
        var query = new GetFilteredVolunteersWithPaginationQuery(
            Id: null,
            Name: "John",
            PositionFrom: 50,
            PositionTo: 100,
            SortBy: "name",
            SortDirection: "asc",
            Page: 1,
            PageSize: 10);

        // Act & Assert
        _validator.TestValidate(query).ShouldNotHaveValidationErrorFor(x => x.PositionFrom);
    }

    [Fact]
    public void Validate_WhenPositionToIsValid_ShouldNotHaveValidationError()
    {
        // Arrange
        var query = new GetFilteredVolunteersWithPaginationQuery(
            Id: null,
            Name: "John",
            PositionFrom: 0,
            PositionTo: 50,
            SortBy: "name",
            SortDirection: "asc",
            Page: 1,
            PageSize: 10);

        // Act & Assert
        _validator.TestValidate(query).ShouldNotHaveValidationErrorFor(x => x.PositionTo);
    }

    /*[Fact]
    public void Validate_WhenPositionFromIsLessThanMin_ShouldHaveValidationError()
    {
        // Arrange
        var query = new GetFilteredVolunteersWithPaginationQuery(
            Id: null,
            Name: "John",
            PositionFrom: Constants.POSITION_MIN_VALUE - 1,
            PositionTo: 100,
            SortBy: "name",
            SortDirection: "asc",
            Page: 1,
            PageSize: 10);

        // Act & Assert
        _validator.TestValidate(query).ShouldHaveValidationErrorFor(x => x.PositionFrom);
    }*/

    /*[Fact]
    public void Validate_WhenPositionToIsGreaterThanMax_ShouldHaveValidationError()
    {
        // Arrange
        var query = new GetFilteredVolunteersWithPaginationQuery(
            Id: null,
            Name: "John",
            PositionFrom: 0,
            PositionTo: Constants.POSITION_MAX_VALUE + 1,
            SortBy: "name",
            SortDirection: "asc",
            Page: 1,
            PageSize: 10);

        // Act & Assert
        _validator.TestValidate(query).ShouldHaveValidationErrorFor(x => x.PositionTo);
    }

    [Fact]
    public void Validate_WhenPositionRangeIsInvalid_ShouldHaveValidationError()
    {
        // Arrange
        var query = new GetFilteredVolunteersWithPaginationQuery(
            Id: null,
            Name: "John",
            PositionFrom: 100,
            PositionTo: 50, // PositionFrom > PositionTo
            SortBy: "name",
            SortDirection: "asc",
            Page: 1,
            PageSize: 10);

        // Act & Assert
        _validator.TestValidate(query).ShouldHaveValidationError()
            .WithErrorCode("invalid.range")
            .WithErrorMessage("PositionFrom must be less than or equal to PositionTo");
    }*/

    // ==================== SORTING ВАЛИДАЦИЯ ====================

    [Fact]
    public void Validate_WhenSortByIsNull_ShouldNotHaveValidationError()
    {
        // Arrange
        var query = new GetFilteredVolunteersWithPaginationQuery(
            Id: null,
            Name: "John",
            PositionFrom: 0,
            PositionTo: 100,
            SortBy: null,
            SortDirection: "asc",
            Page: 1,
            PageSize: 10);

        // Act & Assert
        _validator.TestValidate(query).ShouldNotHaveValidationErrorFor(x => x.SortBy);
    }

    [Fact]
    public void Validate_WhenSortByIsValid_ShouldNotHaveValidationError()
    {
        // Arrange
        var query = new GetFilteredVolunteersWithPaginationQuery(
            Id: null,
            Name: "John",
            PositionFrom: 0,
            PositionTo: 100,
            SortBy: "name",
            SortDirection: "asc",
            Page: 1,
            PageSize: 10);

        // Act & Assert
        _validator.TestValidate(query).ShouldNotHaveValidationErrorFor(x => x.SortBy);
    }

    [Fact]
    public void Validate_WhenSortByIsInvalid_ShouldHaveValidationError()
    {
        // Arrange
        var query = new GetFilteredVolunteersWithPaginationQuery(
            Id: null,
            Name: "John",
            PositionFrom: 0,
            PositionTo: 100,
            SortBy: "invalid_field",
            SortDirection: "asc",
            Page: 1,
            PageSize: 10);

        // Act & Assert
        _validator.TestValidate(query).ShouldHaveValidationErrorFor(x => x.SortBy)
            .WithErrorCode("value.is.invalid")
            .WithErrorMessage("SortBy is invalid");
    }

    [Fact]
    public void Validate_WhenSortDirectionIsNull_ShouldNotHaveValidationError()
    {
        // Arrange
        var query = new GetFilteredVolunteersWithPaginationQuery(
            Id: null,
            Name: "John",
            PositionFrom: 0,
            PositionTo: 100,
            SortBy: "name",
            SortDirection: null,
            Page: 1,
            PageSize: 10);

        // Act & Assert
        _validator.TestValidate(query).ShouldNotHaveValidationErrorFor(x => x.SortDirection);
    }

    [Fact]
    public void Validate_WhenSortDirectionIsValid_ShouldNotHaveValidationError()
    {
        // Arrange
        var query = new GetFilteredVolunteersWithPaginationQuery(
            Id: null,
            Name: "John",
            PositionFrom: 0,
            PositionTo: 100,
            SortBy: "name",
            SortDirection: "desc",
            Page: 1,
            PageSize: 10);

        // Act & Assert
        _validator.TestValidate(query).ShouldNotHaveValidationErrorFor(x => x.SortDirection);
    }

    [Fact]
    public void Validate_WhenSortDirectionIsInvalid_ShouldHaveValidationError()
    {
        // Arrange
        var query = new GetFilteredVolunteersWithPaginationQuery(
            Id: null,
            Name: "John",
            PositionFrom: 0,
            PositionTo: 100,
            SortBy: "name",
            SortDirection: "invalid_direction",
            Page: 1,
            PageSize: 10);

        // Act & Assert
        _validator.TestValidate(query).ShouldHaveValidationErrorFor(x => x.SortDirection)
            .WithErrorCode("value.is.invalid")
            .WithErrorMessage("SortDirection is invalid");
    }

    // ==================== PAGINATION ВАЛИДАЦИЯ ====================

    [Fact]
    public void Validate_WhenPageIsZero_ShouldHaveValidationError()
    {
        // Arrange
        var query = new GetFilteredVolunteersWithPaginationQuery(
            Id: null,
            Name: "John",
            PositionFrom: 0,
            PositionTo: 100,
            SortBy: "name",
            SortDirection: "asc",
            Page: 0,
            PageSize: 10);

        // Act & Assert
        _validator.TestValidate(query).ShouldHaveValidationErrorFor(x => x.Page)
            .WithErrorCode("value.must.be.greater.than.zero")
            .WithErrorMessage("Page must be greater than 0");
    }

    [Fact]
    public void Validate_WhenPageIsNegative_ShouldHaveValidationError()
    {
        // Arrange
        var query = new GetFilteredVolunteersWithPaginationQuery(
            Id: null,
            Name: "John",
            PositionFrom: 0,
            PositionTo: 100,
            SortBy: "name",
            SortDirection: "asc",
            Page: -1,
            PageSize: 10);

        // Act & Assert
        _validator.TestValidate(query).ShouldHaveValidationErrorFor(x => x.Page);
    }

    /*[Fact]
    public void Validate_WhenPageSizeIsTooSmall_ShouldHaveValidationError()
    {
        // Arrange
        var query = new GetFilteredVolunteersWithPaginationQuery(
            Id: null,
            Name: "John",
            PositionFrom: 0,
            PositionTo: 100,
            SortBy: "name",
            SortDirection: "asc",
            Page: 1,
            PageSize: Constants.MIN_PAGE_SIZE - 1);

        // Act & Assert
        _validator.TestValidate(query).ShouldHaveValidationErrorFor(x => x.PageSize);
    }*/

    /*[Fact]
    public void Validate_WhenPageSizeIsTooLarge_ShouldHaveValidationError()
    {
        // Arrange
        var query = new GetFilteredVolunteersWithPaginationQuery(
            Id: null,
            Name: "John",
            PositionFrom: 0,
            PositionTo: 100,
            SortBy: "name",
            SortDirection: "asc",
            Page: 1,
            PageSize: Constants.MAX_PAGE_SIZE + 1);

        // Act & Assert
        _validator.TestValidate(query).ShouldHaveValidationErrorFor(x => x.PageSize);
    }

    // ==================== COMPLEX VALIDATION ====================

    [Fact]
    public void Validate_WhenNoFiltersProvided_ShouldHaveValidationError()
    {
        // Arrange
        var query = new GetFilteredVolunteersWithPaginationQuery(
            Id: null,
            Name: null,
            PositionFrom: null,
            PositionTo: null,
            SortBy: null,
            SortDirection: null,
            Page: 1,
            PageSize: 10);

        // Act & Assert
        _validator.TestValidate(query).ShouldHaveValidationError()
            .WithErrorCode("volunteer.at.least.one.filter.required")
            .WithErrorMessage("At least one filter criterion must be provided");
    }*/

    /*[Fact]
    public void Validate_WhenAtLeastOneFilterProvided_ShouldNotHaveValidationError()
    {
        // Arrange
        var queries = new[]
        {
            // Только ID
            new GetFilteredVolunteersWithPaginationQuery(
                Id: Guid.NewGuid(), Name: null, PositionFrom: null, PositionTo: null,
                SortBy: null, SortDirection: null, Page: 1, PageSize: 10),

            // Только Name
            new GetFilteredVolunteersWithPaginationQuery(
                Id: null, Name: "John", PositionFrom: null, PositionTo: null,
                SortBy: null, SortDirection: null, Page: 1, PageSize: 10),

            // Только PositionFrom
            new GetFilteredVolunteersWithPaginationQuery(
                Id: null, Name: null, PositionFrom: 0, PositionTo: null,
                SortBy: null, SortDirection: null, Page: 1, PageSize: 10),

            // Только PositionTo
            new GetFilteredVolunteersWithPaginationQuery(
                Id: null, Name: null, PositionFrom: null, PositionTo: 100,
                SortBy: null, SortDirection: null, Page: 1, PageSize: 10)
        };

        // Act & Assert
        foreach (var query in queries)
        {
            _validator.TestValidate(query).ShouldNotHaveValidationError();
        }
    }*/

    [Fact]
    public void Validate_WhenAllFieldsValid_ShouldPassValidation()
    {
        // Arrange
        var query = new GetFilteredVolunteersWithPaginationQuery(
            Id: Guid.NewGuid(),
            Name: "John Doe",
            PositionFrom: 0,
            PositionTo: 100,
            SortBy: "name",
            SortDirection: "asc",
            Page: 1,
            PageSize: 10);

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }
}

public static class TestConstants
{
    public const string VALID_NAME = "John Doe";
    public const string TOO_LONG_NAME = "Lorem ipsum dolor sit amet consectetur adipiscing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua";
    public static readonly Guid VALID_GUID = Guid.NewGuid();
}