using Microsoft.AspNetCore.Mvc;
using PetHelp.API.Controllers.Volunteer.Requests;
using PetHelp.API.Extensions;
using PetHelp.Application.VolunteerManagement.CreateVolunteer;

namespace PetHelp.API.Controllers.Volunteer;

public class VolunteerController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateVolunteerCommand(
            request.Name,
            request.Surname,
            request.Patronymik,
            request.Email,
            request.Description,
            request.ExperienceInYears,
            request.PhoneNumber,
            request.Details,
            request.Networks);
        var result = await handler.Handler(command, cancellationToken);

        return result.ToResponse();
    }
}