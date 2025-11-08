using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace PetHelp.Application.PetManagement.Queries.GetPetsWithPagination;

public class GetFilteredPetsWithPaginationQueryValidation : AbstractValidator<GetFilteredPetsWithPaginationQuery>
{
    public GetFilteredPetsWithPaginationQueryValidation()
    {
        
    }
}