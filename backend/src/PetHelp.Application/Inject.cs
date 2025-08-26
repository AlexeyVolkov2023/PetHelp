using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetHelp.Application.VolunteerManagement.AddPet;
using PetHelp.Application.VolunteerManagement.Create;
using PetHelp.Application.VolunteerManagement.Delete.Hard;
using PetHelp.Application.VolunteerManagement.Delete.Soft;
using PetHelp.Application.VolunteerManagement.GetPresignedUrl;
using PetHelp.Application.VolunteerManagement.RemoveFile;
using PetHelp.Application.VolunteerManagement.UpdateMainInfo;
using PetHelp.Application.VolunteerManagement.UpdatePaymentDetail;
using PetHelp.Application.VolunteerManagement.UpdateSocialNetwork;
using PetHelp.Application.VolunteerManagement.UploadFilesToPet;


namespace PetHelp.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateHandler>();
        services.AddScoped<UpdateMainInfoHandler>();
        services.AddScoped<UpdatePaymentDetailHandler>();
        services.AddScoped<SoftDeleteHandler>();
        services.AddScoped<HardDeleteHandler>();
        services.AddScoped<UpdateSocialNetworkHandler>();
        services.AddScoped<AddPetHandler>();
        services.AddScoped<GetPresignedUrlHandler>();
        services.AddScoped<RemoveFileHandler>();
        services.AddScoped<UploadFilesToPetHandler>();
        

        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

        return services;
    }
}