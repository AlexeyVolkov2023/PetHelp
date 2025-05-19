using Microsoft.EntityFrameworkCore.Diagnostics;

namespace PetHelp.Infrastructure.Interceptors;

public class SoftDeleteInterceptors : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}