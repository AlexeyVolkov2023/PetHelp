using System.Linq.Expressions;
using PetHelp.Application.Abstraction;
using PetHelp.Application.Database;
using PetHelp.Application.Dto;
using PetHelp.Application.Extensions;
using PetHelp.Application.Models;

namespace PetHelp.Application.PetManagement.Queries.GetVolunteersWithPaginationQuery;


    public class GetVolunteersWithPaginationHandler : IQueryHandler<PagedList<VolunteerDto>, GetFilteredVolunteersWithPaginationQuery>
    {
        private readonly IReadDbContext _readDbContext;

        public GetVolunteersWithPaginationHandler(IReadDbContext readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public async Task<PagedList<VolunteerDto>> Handle(
            GetFilteredVolunteersWithPaginationQuery query,
            CancellationToken cancellationToken)
        {
            var volunteersQuery = _readDbContext.Volunteers;
        
            Expression<Func<VolunteerDto, object>> keySelector = query.SortBy?.ToLower() switch
            {
                "name" => volunteer => volunteer.FullName.Name,
                _ => pet => pet.Id
            };

            volunteersQuery = query.SortDirection?.ToLower() == "desc" 
                ? volunteersQuery.OrderByDescending(keySelector)
                : volunteersQuery.OrderBy(keySelector);

            volunteersQuery = volunteersQuery.WhereIf(
                !string.IsNullOrWhiteSpace(query.Name),
                v => v.FullName.Name.Contains(query.Name!));

            return await volunteersQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);
        }
    }
