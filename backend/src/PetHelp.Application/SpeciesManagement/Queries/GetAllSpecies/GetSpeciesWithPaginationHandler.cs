using System.Linq.Expressions;
using PetHelp.Application.Abstraction;
using PetHelp.Application.Database;
using PetHelp.Application.Dto;
using PetHelp.Application.Extensions;
using PetHelp.Application.Models;

namespace PetHelp.Application.SpeciesManagement.Queries.GetAllSpecies;

public class GetSpeciesWithPaginationHandler : IQueryHandler<PagedList<SpeciesDto>, GetSpeciesWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetSpeciesWithPaginationHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    
    public async Task<PagedList<SpeciesDto>> Handle(
        GetSpeciesWithPaginationQuery query,
        CancellationToken cancellationToken )
    {
        var speciesQuery = _readDbContext.Species;
        
        Expression<Func<SpeciesDto, object>> keySelector = query.SortBy?.ToLower() switch
        {
            "title" => species => species.Title,
            _ => species => species.Id
        };

        speciesQuery = query.SortDirection?.ToLower() == "desc" 
            ? speciesQuery.OrderByDescending(keySelector)
            : speciesQuery.OrderBy(keySelector);

        speciesQuery = speciesQuery.WhereIf(
            !string.IsNullOrWhiteSpace(query.Title),
            v => v.Title.Contains(query.Title!));

        return await speciesQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}