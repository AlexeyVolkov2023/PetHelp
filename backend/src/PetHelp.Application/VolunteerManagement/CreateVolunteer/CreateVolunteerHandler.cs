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
        var detailsResult = request.Details?
            .Select(d => PaymentDetail.Create(d.Title, d.Description).Value)
            .ToList();

        var networksResult = request.Networks?
            .Select(n => SocialNetwork.Create(n.Name, n.Link).Value)
            .ToList();
        
        var volunteer = await _volunteersRepository.GetByNumber(request.PhoneNumber, cancellationToken);

        if (volunteer.IsSuccess)
        {
            return Errors.Volunteer.AlreadyExist();
        }

        var volunteerId = VolunteerId.NewVolunteerId();

        var volunteerToCreate = new Volunteer(
            volunteerId,
            request.FullName,
            request.Email,
            request.Description,
            request.ExperienceInYears,
            request.PhoneNumber,
            detailsResult,
            networksResult);

        await _volunteersRepository.Add(volunteerToCreate, cancellationToken);
        
        return volunteerToCreate.Id.Value;
    }
}