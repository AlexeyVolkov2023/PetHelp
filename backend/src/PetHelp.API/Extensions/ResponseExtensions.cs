using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using PetHelp.API.Response;
using PetHelp.Domain.Shared;


namespace PetHelp.API.Extensions;

public static class ResponseExtensions
{
   public static ActionResult<T> ToResponse<T>(this Result<T, Error> result)
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(Envelope.Ok(result.Value));
        }
        var statusCode = result.Error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };
        var envelope = Envelope.Error(result.Error);
        
        return new ObjectResult(envelope)
        {
            StatusCode = statusCode
        };
    }
    
    public static ActionResult ToResponse(this UnitResult<Error> result)
    {
        if (result.IsSuccess)
        {
            return new OkResult();
        }
        var statusCode = result.Error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };
        var envelope = Envelope.Error(result.Error);
        
        return new ObjectResult(envelope)
        {
            StatusCode = statusCode
        };
    }
}