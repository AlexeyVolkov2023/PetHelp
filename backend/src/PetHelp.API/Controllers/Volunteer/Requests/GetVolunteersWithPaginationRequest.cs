using PetHelp.Application.PetManagement.Queries.GetVolunteersWithPaginationQuery;

namespace PetHelp.API.Controllers.Volunteer.Requests;

public record GetVolunteersWithPaginationRequest(
    Guid? Id,
    string? Name,
    int? PositionTo,
    int? PositionFrom,
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize)
{
    public GetFilteredVolunteersWithPaginationQuery ToQuery() => new(
        Id,
        Name,
        PositionTo,
        PositionFrom,
        SortBy,
        SortDirection,
        Page,
        PageSize);
}