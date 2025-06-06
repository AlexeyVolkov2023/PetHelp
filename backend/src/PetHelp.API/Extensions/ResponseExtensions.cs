﻿using CSharpFunctionalExtensions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PetHelp.API.Response;
using PetHelp.Domain.Shared;

namespace PetHelp.API.Extensions;

public static class ResponseExtensions
{
    public static ActionResult ToResponse(this Error error)
    {
        var statusCode = GetStatusCodeForErrorType(error.Type);

        var envelope = Envelope.Error(error.ToErrorList());

        return new ObjectResult(envelope)
        {
            StatusCode = statusCode
        };
    }

    public static ActionResult ToResponse(this ErrorList errors)
    {
        if (!errors.Any())
        {
            return new ObjectResult(Envelope.Error(errors))
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }

        var distinctErrorTypes = errors
            .Select(x => x.Type)
            .Distinct()
            .ToList();

        var statusCode = distinctErrorTypes.Count > 1
            ? StatusCodes.Status500InternalServerError
            : GetStatusCodeForErrorType(distinctErrorTypes.First());

        var envelope = Envelope.Error(errors);

        return new ObjectResult(Envelope.Error(errors))
        {
            StatusCode = statusCode
        };
    }

    private static int GetStatusCodeForErrorType(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };
}