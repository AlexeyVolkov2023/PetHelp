using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetHelp.Application.VolunteerManagement.CreateVolunteer;

namespace PetHelp.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();

        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

        return services;
    }
}