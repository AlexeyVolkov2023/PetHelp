using Microsoft.AspNetCore.Mvc;
using PetHelp.API.Controllers.Species.Requests;
using PetHelp.Application.SpeciesManagement.Queries.GetBreedsBySpeciesId;

namespace PetHelp.API.Controllers.Breed;

public class BreedController : ApplicationController
{
    [HttpGet("species/{speciesId:guid}/breeds")]
    public async Task<IActionResult> GetBreedsBySpeciesId(
        [FromRoute] Guid speciesId,
        [FromQuery] GetBreedsBySpeciesIdRequest request,
        [FromServices] GetBreedsBySpeciesIdHandler handler,
        CancellationToken cancellationToken)
    {
        var query = request.ToQuery(speciesId);

        var responce = await handler.Handle(query, cancellationToken);

        return Ok(responce);
    }
}