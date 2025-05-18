using Microsoft.EntityFrameworkCore;
using PetHelp.Infrastructure;

namespace PetHelp.API.Extensions;

public static class AppExtensions
{
    public static async Task ApplyMigration(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}