using CSharpFunctionalExtensions;
using PetHelp.Domain.AnimalManagement.AggregateRoot;
using PetHelp.Domain.AnimalManagement.ID;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.PetManagement;

public interface IVolunteersRepository
{
    Task<Result<Volunteer, Error>> GetById(
        VolunteerId volunteerId,
        CancellationToken cancellationToken = default);

    Task<Result<Volunteer, Error>>  GetByNumber(
        string requestPhoneNumber,
        CancellationToken cancellationToken);
   
}