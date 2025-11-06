using PetHelp.Application.Abstraction;

namespace PetHelp.Application.PetManagement.Queries.GetVolunteersWithPaginationQuery;

public record GetFilteredVolunteersWithPaginationQuery(
    Guid? Id,
    string? Name,
    int? PositionTo,
    int? PositionFrom,
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize) : IQuery;