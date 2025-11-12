using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetHelp.Application.Abstraction;
using PetHelp.Application.Database;
using PetHelp.Domain.Shared;

namespace PetHelp.Application.SpeciesManagement.Commands.DeleteSpeciesBreed;

public class DeleteSpeciesBreedHandler : ICommandHandler<Guid, DeleteSpeciesBreedCommand>
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IApplicationDbContext _writeDbContext;
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<DeleteSpeciesBreedHandler> _logger;

    public DeleteSpeciesBreedHandler(
        IReadDbContext readDbContext,
        IApplicationDbContext writeDbContext,
        ILogger<DeleteSpeciesBreedHandler> logger,
        ISpeciesRepository speciesRepository)
    {
        _readDbContext = readDbContext;
        _writeDbContext = writeDbContext;
        _logger = logger;
        _speciesRepository = speciesRepository;
    }


    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteSpeciesBreedCommand command,
        CancellationToken cancellationToken = default)
    {
        var speciesResult = await _speciesRepository.GetById(command.SpeciesId, cancellationToken);
        if (speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();

        var species = speciesResult.Value;

        var isSpeciesUsed = await _readDbContext.Pets
            .AnyAsync(p => p.SpeciesBreed.SpeciesId == command.SpeciesId, cancellationToken);

        if (isSpeciesUsed == false)
        {
            _writeDbContext.Species.Remove(species);

            await _writeDbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Deleted species {SpeciesId} (not used by any pets)", command.SpeciesId);

            return command.SpeciesId;
        }

        var isBreedUsed = await _readDbContext.Pets
            .AnyAsync(p => p.SpeciesBreed.BreedId == command.BreedId, cancellationToken);

        if (isBreedUsed == false)
        {
            species.RemoveBreed(command.BreedId);

            await _writeDbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Deleted breed {BreedId} from species {SpeciesId} (not used by any pets)",
                command.BreedId, command.SpeciesId);

            return command.BreedId;
        }

        _logger.LogError("Cannot delete species {SpeciesId} or breed {BreedId} - both are used by pets",
            command.BreedId, command.SpeciesId);

        return Errors.Species.IsUsed();
    }
}