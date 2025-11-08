using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHelp.Application.Abstraction;
using PetHelp.Application.Database;
using PetHelp.Application.Extensions;
using PetHelp.Domain.AnimalManagement.ID;
using PetHelp.Domain.AnimalManagement.VO;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.PetManagement.Commands.Create;

public class CreateHandler : ICommandHandler<Guid, CreateCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IApplicationDbContext _dbContext;
    private readonly IValidator<CreateCommand> _validator;
    private readonly ILogger<CreateHandler> _logger;

    public CreateHandler(
        IVolunteersRepository volunteersRepository,
        IApplicationDbContext dbContext,
        IValidator<CreateCommand> validator,
        ILogger<CreateHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _dbContext = dbContext;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        CreateCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var fullName = FullName.Create(
            command.FullNameDto.Name,
            command.FullNameDto.Surname,
            command.FullNameDto.Patronymic!).Value;

        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;

        var email = Email.Create(command.Email).Value;
       
        var description = Description.Create(command.Description).Value;

        var experienceInYears = ExperienceInYears.Create(command.ExperienceInYears).Value;
        
        var detailsResult = command.PaymentDetails?
            .Select(d => PaymentDetail.Create(d.Title, d.Description).Value)
            .ToList();
       
        var networksResult = command.SocialNetworks?
            .Select(n => SocialNetwork.Create(n.Name, n.Link).Value)
            .ToList();
        
        var volunteer = await _volunteersRepository.GetByNumber(phoneNumber.Value, cancellationToken);

        if (volunteer.IsSuccess)
            return Errors.Volunteer.AlreadyExist();

        var volunteerId = VolunteerId.NewVolunteerId();

        var volunteerToCreate = new Domain.AnimalManagement.AggregateRoot.Volunteer(
            volunteerId,
            fullName,
            email,
            description,
            experienceInYears,
            phoneNumber,
            detailsResult,
            networksResult);

        await _dbContext.Volunteers.AddAsync(volunteerToCreate, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Created volunteer with Id {volunteerId}", volunteerId);

        return (Guid)volunteerToCreate.Id;
    }
}