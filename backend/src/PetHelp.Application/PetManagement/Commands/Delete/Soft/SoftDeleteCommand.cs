using PetHelp.Application.Abstraction;

namespace PetHelp.Application.PetManagement.Commands.Delete.Soft;

public record SoftDeleteCommand(Guid VolunteerId) : ICommand;