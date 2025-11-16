using PetHelp.Application.Abstraction;

namespace PetHelp.Application.SpeciesManagement.Queries.GetAllSpecies;

public record GetSpeciesWithPaginationQuery(
    Guid? Id,
    string? Title,
    int? PositionTo,
    int? PositionFrom,
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize) : IQuery;