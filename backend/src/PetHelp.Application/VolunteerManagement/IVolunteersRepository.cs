using CSharpFunctionalExtensions;
using PetHelp.Domain.AnimalManagement.AggregateRoot;
using PetHelp.Domain.AnimalManagement.ID;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.VolunteerManagement;

public interface IVolunteersRepository
{
    Task<Guid> Add(
        Volunteer volunteer,
        CancellationToken cancellationToken = default);

    Task<Result<Volunteer, Error>> GetById(
        VolunteerId volunteerId,
        CancellationToken cancellationToken = default);

    Task<Result<Volunteer, Error>>  GetByNumber(
        string requestPhoneNumber,
        CancellationToken cancellationToken);
    
    Task<Guid> Save(
        Volunteer volunteer,
        CancellationToken cancellationToken = default);  
    
    Task<Guid> SoftDelete(
        Domain.AnimalManagement.AggregateRoot.Volunteer volunteer,
        CancellationToken cancellationToken = default);
    Task<Guid> HardDelete(
        Domain.AnimalManagement.AggregateRoot.Volunteer volunteer,
        CancellationToken cancellationToken = default);
}