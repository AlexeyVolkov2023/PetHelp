using PetHelp.Application.Abstraction;

namespace PetHelp.Application.PetManagement.Queries.GetPetsWithPagination;

public record GetFilteredPetsWithPaginationQuery(
    string? PhoneNumber,
    int? PositionTo,
    int? PositionFrom,
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize) : IQuery;