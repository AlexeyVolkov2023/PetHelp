using CSharpFunctionalExtensions;
using PetHelp.Domain.AnimalManagement.AggregateRoot;
using PetHelp.Domain.AnimalManagement.ID;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.VolunteerManagement;

public interface IVolunteersRepository
{
    /*
    Task<Guid> Add(
        Volunteer volunteer,
        CancellationToken cancellationToken = default);
        */

    Task<Result<Volunteer, Error>> GetById(
        VolunteerId volunteerId,
        CancellationToken cancellationToken = default);

    Task<Result<Volunteer, Error>>  GetByNumber(
        string requestPhoneNumber,
        CancellationToken cancellationToken);
    
    /*Guid Save(
        Volunteer volunteer,
        CancellationToken cancellationToken = default);  
    
    Guid SoftDelete(
        Volunteer volunteer,
        CancellationToken cancellationToken = default);
    
    Guid HardDelete(
        Volunteer volunteer,
        CancellationToken cancellationToken = default);*/
}