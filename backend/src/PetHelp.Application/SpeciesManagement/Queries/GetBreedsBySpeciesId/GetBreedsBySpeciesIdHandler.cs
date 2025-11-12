using System.Linq.Expressions;
using PetHelp.Application.Abstraction;
using PetHelp.Application.Database;
using PetHelp.Application.Dto;
using PetHelp.Application.Models;
using PetHelp.Application.Extensions;


namespace PetHelp.Application.SpeciesManagement.Queries.GetBreedsBySpeciesId;

public class GetBreedsBySpeciesIdHandler : IQueryHandler<PagedList<BreedDto>, GetBreedsBySpeciesIdQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetBreedsBySpeciesIdHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    
    public async Task<PagedList<BreedDto>> Handle(
        GetBreedsBySpeciesIdQuery query,
        CancellationToken cancellationToken )
    {
        var breedsQuery = _readDbContext.Breeds;
        
        Expression<Func<BreedDto, object>> keySelector = query.SortBy?.ToLower() switch
        {
            "title" => breeds => breeds.Title,
            _ => breed => breed.Id
        };

        breedsQuery = query.SortDirection?.ToLower() == "desc" 
            ? breedsQuery.OrderByDescending(keySelector)
            : breedsQuery.OrderBy(keySelector);

        breedsQuery = breedsQuery.WhereIf(
            !string.IsNullOrWhiteSpace(query.Title),
            v => v.Title.Contains(query.Title!));
        
        breedsQuery = breedsQuery.Where(b => b.SpeciesId == query.SpeciesId);

        return await breedsQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}