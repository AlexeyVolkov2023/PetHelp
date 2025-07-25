using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetHelp.API.Controllers.Volunteer.Requests;
using PetHelp.API.Extensions;
using PetHelp.Application.Dto;
using PetHelp.Application.VolunteerManagement.AddPet;
using PetHelp.Application.VolunteerManagement.Create;
using PetHelp.Application.VolunteerManagement.Delete.Hard;
using PetHelp.Application.VolunteerManagement.Delete.Soft;
using PetHelp.Application.VolunteerManagement.GetPresignedUrl;
using PetHelp.Application.VolunteerManagement.RemoveFile;
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

    [HttpPatch("{id:guid}/payment-detail)")]
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

    [HttpPatch("{id:guid}/social-network)")]
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

    [HttpDelete("soft/{id:guid}")]
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

    [HttpPost("{id:guid}/pet")]
    public async Task<ActionResult> AddPet(
        [FromRoute] Guid id,
        [FromForm] AddPetRequest request,
        [FromServices] AddPetHandler handler,
        CancellationToken cancellationToken = default)
    {
        List<CreateFileDto> filesDto = [];
        
        try
        {
            foreach (var file in request.Files)
            {
                var stream = file.OpenReadStream();
                filesDto.Add(new CreateFileDto(stream,file.FileName, file.ContentType));
            }
            
            var command = new AddPetCommand(
                id,
                request.PetInfoDto,
                request.PetDataDto,
                request.AddressDto,
                request.PhoneNumber,
                request.Status,
                request.DateOfBirth,
                request.SpeciesId,
                request.BreedId,
                filesDto,
                request.PaymentDetails);

            var result = await handler.Handle(command, cancellationToken);
            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }
        finally
        {
            foreach (var fileDto in filesDto)
            {
                await fileDto.Content.DisposeAsync();
            }
        }
    }
    
    [HttpGet("files/{bucketName}/{objectName}/url")]
    public async Task<ActionResult> GetFileUrl(
        [FromRoute] string bucketName,
        [FromRoute] string objectName,
        [FromServices] GetPresignedUrlHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new GetPresignedUrlCommand(
            bucketName,
            objectName);

        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    [HttpDelete("files/{bucketName}/{objectPath}")]
    public async Task<IActionResult> RemoveFile(
        [FromRoute] string bucketName,
        [FromRoute] string objectPath,
        [FromServices] RemoveFileHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new RemoveFileCommand(
            bucketName,
            objectPath);
        
        var result = await handler.Handle(command, cancellationToken);
    
        if (result.IsFailure)
            return result.Error.ToResponse();
    
        return Ok(objectPath);
    }
}