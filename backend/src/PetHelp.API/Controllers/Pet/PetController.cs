using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetHelp.API.Controllers.Pet.Requests;
using PetHelp.Application.PetManagement.Queries.GetPetsWithPagination;
using PetHelp.Application.PetManagement.Queries.GetVolunteersWithPaginationQuery;

namespace PetHelp.API.Controllers.Pet;

public class PetController : ApplicationController
{
    [HttpGet]
    public async Task<ActionResult> Get(
        [FromQuery] GetPetWithPaginationRequest request,
        [FromServices] GetPetsWithPaginationHandler handler,
        [FromServices] IValidator<GetFilteredVolunteersWithPaginationQuery> validator,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery();

        var responce = await handler.Handle(query, cancellationToken);

        return Ok(responce);
    }
}