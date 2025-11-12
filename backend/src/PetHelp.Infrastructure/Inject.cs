using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetHelp.Application.Database;
using PetHelp.Application.Files;
using PetHelp.Application.Messaging;
using PetHelp.Application.PetManagement;
using PetHelp.Application.SpeciesManagement;
using PetHelp.Infrastructure.BackgroundServices;
using PetHelp.Infrastructure.DbContexts;
using PetHelp.Infrastructure.Files;
using PetHelp.Infrastructure.MessagesQueues;
using PetHelp.Infrastructure.Options;
using PetHelp.Infrastructure.Providers;
using PetHelp.Infrastructure.Repositories;
using FileInfo = PetHelp.Application.Files.FileInfo;

namespace PetHelp.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDbContext()
            .AddMinio(configuration)
            .AddRepository()
            .AddHostedServices()
            .AddMessageQueque()
            .AddServices();

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
    
    private static IServiceCollection AddDbContext(this IServiceCollection services)
    {
        services.AddScoped<IApplicationDbContext, WriteDbContext>();
        services.AddScoped<IReadDbContext, ReadDbContext>();

        return services;
    } 
    
    private static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IVolunteersRepository, VolunteersRepository>();
        services.AddScoped<ISpeciesRepository, SpeciesRepository>();

        return services;
    }
    
    private static IServiceCollection AddHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<FilesCleanerBackgroundServices>();

        return services;
    }
    
    private static IServiceCollection AddMessageQueque(this IServiceCollection services)
    {
        services.AddSingleton<IMessageQueue<IEnumerable<FileInfo>>, InMemoryMessageQueue<IEnumerable<FileInfo>>>();

        return services;
    }
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IFilesCleanerService, FilesCleanerService>();

        return services;
    }
}