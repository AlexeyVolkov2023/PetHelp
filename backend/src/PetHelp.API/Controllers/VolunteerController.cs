

using Microsoft.AspNetCore.Mvc;
using PetHelp.API.Extensions;
using PetHelp.Application.VolunteerManagement.CreateVolunteer;

namespace PetHelp.API.Controllers;

public class VolunteerController : ApplicationController
{
    [HttpPost]
    
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var  result = await handler.Handler(request, cancellationToken);

        return result.ToResponse();
    }
}