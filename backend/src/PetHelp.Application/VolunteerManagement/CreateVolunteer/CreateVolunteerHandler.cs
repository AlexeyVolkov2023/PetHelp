using CSharpFunctionalExtensions;
using PetHelp.Domain.AnimalManagement.AggregateRoot;
using PetHelp.Domain.AnimalManagement.ID;
using PetHelp.Domain.AnimalManagement.VO;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.VolunteerManagement.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;

    public CreateVolunteerHandler(IVolunteersRepository volunteersRepository)
    {
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<Guid, Error>> Handler(
        CreateVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.NewVolunteerId();
        
        var volunteer = await _volunteersRepository.GetByNumber(command.PhoneNumber, cancellationToken);

        if (volunteer.IsSuccess)
        {
            return Errors.Volunteer.AlreadyExist();
        }
        
        var fullNameResult =
            FullName.Create(command.Name,
                            command.Surname,
                            command.Patronymik!);
        if (fullNameResult.IsFailure)
            return fullNameResult.Error;
        
        var emailResult = Email.Create(command.Email);
        if (emailResult.IsFailure)
            return emailResult.Error;
        
        var descriptionResult = Description.Create(command.Description);
        if (descriptionResult.IsFailure)
            return descriptionResult.Error;
        
        var experienceInYearsResult = ExperienceInYears.Create(command.ExperienceInYears);
        if (experienceInYearsResult.IsFailure)
            return experienceInYearsResult.Error;
        
        var phoneNumberResult = PhoneNumber.Create(command.PhoneNumber);
        if (phoneNumberResult.IsFailure)
            return phoneNumberResult.Error;

        var detailsResult = command.Details?
            .Select(d => PaymentDetail.Create(d.Title, d.Description).Value)
            .ToList();

        var networksResult = command.Networks?
            .Select(n => SocialNetwork.Create(n.Name, n.Link).Value)
            .ToList();

        var volunteerToCreate = new Volunteer(
            volunteerId,
            fullNameResult.Value,
            emailResult.Value,
            descriptionResult.Value,
            experienceInYearsResult.Value,
            phoneNumberResult.Value,
            detailsResult,
            networksResult);

        await _volunteersRepository.Add(volunteerToCreate, cancellationToken);

        return volunteerToCreate.Id.Value;
    }
}