using Microsoft.AspNetCore.Mvc;
using PetHelp.API.Controllers.Species.Requests;
using PetHelp.API.Extensions;
using PetHelp.Application.SpeciesManagement.Commands.DeleteSpeciesBreed;
using PetHelp.Application.SpeciesManagement.Queries.GetAllSpecies;


namespace PetHelp.API.Controllers.Species;

public class SpeciesController : ApplicationController
{
    [HttpDelete("species_breed/{speciesId:guid}/{breedId:guid}")]
    public async Task<IActionResult> DeleteSpeciesOrBreed(
        [FromRoute] Guid speciesId,
        [FromRoute] Guid breedId,
        [FromServices] DeleteSpeciesBreedHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteSpeciesBreedCommand(speciesId, breedId);

        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }


    [HttpGet("species")]
    public async Task<IActionResult> GetAllSpecies(
        [FromQuery] GetAllSpeciesRequest request,
        [FromServices] GetSpeciesWithPaginationHandler handler,
        CancellationToken cancellationToken)
    {
        var query = request.ToQuery();

        var responce = await handler.Handle(query, cancellationToken);

        return Ok(responce);
    }
}