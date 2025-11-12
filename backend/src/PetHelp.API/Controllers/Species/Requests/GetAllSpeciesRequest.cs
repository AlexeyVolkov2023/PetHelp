using PetHelp.Application.SpeciesManagement.Queries.GetAllSpecies;

namespace PetHelp.API.Controllers.Species.Requests;

public record GetAllSpeciesRequest(
    Guid? Id,
    string? Title,
    int? PositionTo,
    int? PositionFrom,
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize)
{
    public GetSpeciesWithPaginationQuery ToQuery() => new(
        Id,
        Title,
        PositionTo,
        PositionFrom,
        SortBy,
        SortDirection,
        Page,
        PageSize);
}