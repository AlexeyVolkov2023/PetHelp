using FluentValidation;
using PetHelp.Application.Validations;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.PetManagement.Queries.GetVolunteersWithPaginationQuery;

public class GetFilteredVolunteersWithPaginationQueryValidation : AbstractValidator<GetFilteredVolunteersWithPaginationQuery>
{
    public GetFilteredVolunteersWithPaginationQueryValidation()
    {
        
    }
}