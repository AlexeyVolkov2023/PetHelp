using System.Security.Cryptography.Xml;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHelp.Application.Extensions;
using PetHelp.Domain.AnimalManagement.AggregateRoot;
using PetHelp.Domain.AnimalManagement.ID;
using PetHelp.Domain.AnimalManagement.VO;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.VolunteerManagement.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IValidator<CreateVolunteerCommand> _validator;
    private readonly ILogger<CreateVolunteerHandler> _logger;

    public CreateVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        IValidator<CreateVolunteerCommand> validator,
        ILogger<CreateVolunteerHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handler(
        CreateVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {

        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }
     
        var volunteerId = VolunteerId.NewVolunteerId();

        var volunteerResult = await _volunteersRepository.GetByNumber(command.PhoneNumber, cancellationToken);
        if (volunteerResult.IsSuccess)
        {
            return Result.Failure<Guid, ErrorList>(Errors.Volunteer.AlreadyExist());
        }

        var fullName = FullName.Create(
            command.FullNameDto.Name,
            command.FullNameDto.Surname,
            command.FullNameDto.Patronymik!).Value;

        var email = Email.Create(command.Email).Value;

        var description = Description.Create(command.Description).Value;

        var experienceInYears = ExperienceInYears.Create(command.ExperienceInYears).Value;

        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;

        var details = command.Details?
            .Select(d => PaymentDetail.Create(d.Title, d.Description).Value)
            .ToList();

        var networks = command.Networks?
            .Select(n => SocialNetwork.Create(n.Name, n.Link).Value)
            .ToList();

        var volunteerToCreate = new Volunteer(
            volunteerId,
            fullName,
            email,
            description,
            experienceInYears,
            phoneNumber,
            details,
            networks);

        await _volunteersRepository.Add(volunteerToCreate, cancellationToken);
        
        _logger.LogInformation("Created volunteer with Id {volunteerId}", volunteerId);

        return volunteerToCreate.Id.Value;
    }
}