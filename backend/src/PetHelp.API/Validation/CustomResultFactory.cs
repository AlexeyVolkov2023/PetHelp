using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PetHelp.API.Response;
using PetHelp.Domain.Shared;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace PetHelp.API.Validation;

/*public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
{
    /*public IActionResult CreateActionResult(
        ActionExecutingContext context, 
        ValidationProblemDetails? validationProblemDetails)
    {
        if (validationProblemDetails == null)
        {
            throw new InvalidOperationException("ValidationProblemDetails is null");
        }

        List<ResponseError> errors = [];

        foreach (var (invalidFields, validationErrors) in validationProblemDetails.Errors)
        {
            var responceErrors = from errorMessage in validationErrors
                let error = Error.Deserialize(errorMessage)
                select new ResponseError(error.Code, error.Message, invalidFields);
            
            errors.AddRange(responceErrors);
        }

        var envelope = Envelope.Error(errors);
        
        return new ObjectResult(envelope)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    }#1#
}*/