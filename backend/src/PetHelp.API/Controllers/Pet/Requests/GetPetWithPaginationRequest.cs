using PetHelp.Application.PetManagement.Queries.GetPetsWithPagination;

namespace PetHelp.API.Controllers.Pet.Requests;

public record GetPetWithPaginationRequest(
    string? PhoneNumber,
    int? PositionTo,
    int? PositionFrom,
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize)
{
    public GetFilteredPetsWithPaginationQuery ToQuery() => new(
        PhoneNumber,
        PositionTo,
        PositionFrom,
        SortBy,
        SortDirection,
        Page,
        PageSize);
}