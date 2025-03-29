using Microsoft.AspNetCore.Mvc;
using PetHelp.Application.Dtos;
using PetHelp.Application.VolunteerManagement.CreateVolunteer;
using PetHelp.API.Controllers.Volunteer.Requests;
using PetHelp.API.Extensions;


namespace PetHelp.API.Controllers.Volunteer;

public class VolunteerController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handler(request.ToCommand(), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }

        return Ok(result.Value);
    }
}