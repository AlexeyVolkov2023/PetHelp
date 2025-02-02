using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetHelp.Application.VolunteerManagement;
using PetHelp.Domain.AnimalManagement.AggregateRoot;
using PetHelp.Domain.AnimalManagement.ID;
using PetHelp.Domain.Shared;

namespace PetHelp.Infrastructure.Repositories;


public class VolunteersRepository(ApplicationDbContext dbContext) : IVolunteersRepository
{
    public async Task<Guid> Add(
        Volunteer volunteer,
        CancellationToken cancellationToken = default)
    {
        await dbContext.Volunteers.AddAsync(volunteer, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return volunteer.Id;
    }
    
    public async Task<Result<Volunteer, Error>> GetById(
        VolunteerId volunteerId,
        CancellationToken cancellationToken = default)
    {
        var volunteer = await dbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.Id == volunteerId, cancellationToken);

        if (volunteer is null)
        {
            return Errors.General.NotFound(volunteerId);
        }

        return volunteer;
    }

    public async Task<Result<Volunteer, Error>> GetByNumber(
        string requestPhoneNumber, 
        CancellationToken cancellationToken)
    {
        var volunteer = await dbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.PhoneNumber == requestPhoneNumber, cancellationToken);
       
        if (volunteer is null)
        {
            return Errors.General.NotFound();
        }

        return volunteer;
    }
}