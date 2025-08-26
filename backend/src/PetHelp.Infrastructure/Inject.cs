using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetHelp.Application;
using PetHelp.Application.Database;
using PetHelp.Application.FileProvider;
using PetHelp.Application.VolunteerManagement;
using PetHelp.Infrastructure.Options;
using PetHelp.Infrastructure.Providers;
using PetHelp.Infrastructure.Repositories;

namespace PetHelp.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IVolunteersRepository, VolunteersRepository>();
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddMinio(configuration);

        return services;
    }

    private static IServiceCollection AddMinio(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<MinioOptions>(
            configuration.GetSection(MinioOptions.MINIO));

        services.AddMinio(options =>
        {
            var minioOptions = configuration
                                   .GetSection(MinioOptions.MINIO)
                                   .Get<MinioOptions>()
                               ?? throw new ApplicationException("MissingMinio configuration");

            options.WithEndpoint(minioOptions.Endpoint);
            options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
            options.WithSSL(minioOptions.WithSsl);
        });

        services.AddScoped<IFileProvider, MinioProvider>();

        return services;
    }
}