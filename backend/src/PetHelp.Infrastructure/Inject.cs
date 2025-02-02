using Microsoft.Extensions.DependencyInjection;
using PetHelp.Application.VolunteerManagement;
using PetHelp.Infrastructure.Repositories;

namespace PetHelp.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ApplicationDbContext>();
        
        services.AddScoped<IVolunteersRepository, VolunteersRepository>();

        return services;
    }
}