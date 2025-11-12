using PetHelp.Application.Abstraction;

namespace PetHelp.Application.SpeciesManagement.Queries.GetBreedsBySpeciesId;

public record GetBreedsBySpeciesIdQuery(
    Guid? SpeciesId,
    string? Title,
    int? PositionTo,
    int? PositionFrom,
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize) : IQuery;