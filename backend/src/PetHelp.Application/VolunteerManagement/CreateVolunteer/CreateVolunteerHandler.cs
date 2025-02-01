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
        CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.NewVolunteerId();
        
        var volunteer = await _volunteersRepository.GetByNumber(request.PhoneNumber, cancellationToken);

        if (volunteer.IsSuccess)
        {
            return Errors.Volunteer.AlreadyExist();
        }
        
        var fullNameResult =
            FullName.Create(request.FullName.Name,
                            request.FullName.Surname,
                            request.FullName.Patronymik!);
        if (fullNameResult.IsFailure)
            return fullNameResult.Error;
        
        var emailResult = Email.Create(request.Email);
        if (emailResult.IsFailure)
            return emailResult.Error;
        
        var descriptionResult = Description.Create(request.Description);
        if (descriptionResult.IsFailure)
            return descriptionResult.Error;
        
        var experienceInYearsResult = ExperienceInYears.Create(request.ExperienceInYears);
        if (experienceInYearsResult.IsFailure)
            return experienceInYearsResult.Error;
        
        var phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);
        if (phoneNumberResult.IsFailure)
            return phoneNumberResult.Error;

        var detailsResult = request.Details?
            .Select(d => PaymentDetail.Create(d.Title, d.Description).Value)
            .ToList();

        var networksResult = request.Networks?
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