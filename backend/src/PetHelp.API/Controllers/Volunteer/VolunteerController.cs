using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetHelp.API.Controllers.Volunteer.Requests;
using PetHelp.API.Extensions;
using PetHelp.Application.Dtos;
using PetHelp.Application.VolunteerManagement.Create;
using PetHelp.Application.VolunteerManagement.Delete;
using PetHelp.Application.VolunteerManagement.Delete.Hard;
using PetHelp.Application.VolunteerManagement.Delete.Soft;
using PetHelp.Application.VolunteerManagement.UpdateMainInfo;
using PetHelp.Application.VolunteerManagement.UpdatePaymentDetail;
using PetHelp.Application.VolunteerManagement.UpdateSocialNetwork;


namespace PetHelp.API.Controllers.Volunteer;

public class VolunteerController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromServices] CreateHandler handler,
        [FromBody] CreateRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand();

        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPatch("{id:guid}/main-info")]
    public async Task<ActionResult> UpdateMainInfo(
        [FromRoute] Guid id,
        [FromServices] UpdateMainInfoHandler handler,
        [FromBody] UpdateMainInfoRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateMainInfoCommand(
            id,
            request.FullNameDto,
            request.Email,
            request.Description,
            request.ExperienceInYears,
            request.PhoneNumber);

        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPatch("{id:guid}/payment_detail)")]
    public async Task<ActionResult> UpdatePaymentDetail(
        [FromRoute] Guid id,
        [FromServices] UpdatePaymentDetailHandler handler,
        [FromBody] UpdatePaymentDetailRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdatePaymentDetailCommand(id, request.PaymentDetails);

        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPatch("{id:guid}/social_network)")]
    public async Task<ActionResult> UpdateSocialNetwork(
        [FromRoute] Guid id,
        [FromServices] UpdateSocialNetworkHandler handler,
        [FromBody] UpdateSocialNetworkRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateSocialNetworkCommand(id, request.SocialNetwork);

        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> SoftDelete(
        [FromRoute] Guid id,
        [FromServices] SoftDeleteHandler handler,
        [FromServices] IValidator<SoftDeleteCommand> validator,
        CancellationToken cancellationToken = default)
    {
        var command = new SoftDeleteCommand(id);

        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    [HttpDelete("hard/{id:guid}")]
    public async Task<ActionResult> HardDelete(
        [FromRoute] Guid id,
        [FromServices] HardDeleteHandler handler,
        [FromServices] IValidator<HardDeleteCommand> validator,
        CancellationToken cancellationToken = default)
    {
        var command = new HardDeleteCommand(id);
        
        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}