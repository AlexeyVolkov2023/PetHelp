using Microsoft.AspNetCore.Mvc;
using PetHelp.Application.SpeciesManagement.Queries.GetBreedsBySpeciesId;

namespace PetHelp.API.Controllers.Species.Requests;

public record GetBreedsBySpeciesIdRequest(
    string? Title,
    int? PositionTo,
    int? PositionFrom,
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize)
{
    public GetBreedsBySpeciesIdQuery ToQuery(Guid speciesId)
    {
        return new GetBreedsBySpeciesIdQuery(
            speciesId,
            Title,
            PositionTo,
            PositionFrom,
            SortBy,
            SortDirection,
            Page,
            PageSize);
    }
}