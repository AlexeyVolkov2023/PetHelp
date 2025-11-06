using System.Linq.Expressions;
using PetHelp.Application.Abstraction;
using PetHelp.Application.Database;
using PetHelp.Application.Dto;
using PetHelp.Application.Extensions;
using PetHelp.Application.Models;

namespace PetHelp.Application.PetManagement.Queries.GetPetsWithPagination;

public class GetPetsWithPaginationHandler : IQueryHandler<PagedList<PetDto>, GetFilteredPetsWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetPetsWithPaginationHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<PagedList<PetDto>> Handle(
        GetFilteredPetsWithPaginationQuery query,
        CancellationToken cancellationToken)
    {
        var petsQuery = _readDbContext.Pets;
        
        Expression<Func<PetDto, object>> keySelector = query.SortBy?.ToLower() switch
        {
            "name" => pet => pet.PetInfo.Name,
            "position" => pet => pet.Position,
            _ => pet => pet.Id
        };

        petsQuery = query.SortDirection?.ToLower() == "desc" 
            ? petsQuery.OrderByDescending(keySelector)
            : petsQuery.OrderBy(keySelector);

        petsQuery = petsQuery.WhereIf(
            !string.IsNullOrWhiteSpace(query.PhoneNumber),
            p => p.PhoneNumber.Contains(query.PhoneNumber!));

        petsQuery = petsQuery.WhereIf(
            query.PositionTo != null,
            i => i.Position <= query.PositionTo);

        petsQuery = petsQuery.WhereIf(
            query.PositionFrom != null,
            i => i.Position >= query.PositionTo);

        return await petsQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}