using PetHelp.Application.Abstraction;

namespace PetHelp.Application.PetManagement.Commands.Delete.Hard;

public record HardDeleteCommand(Guid VolunteerId) : ICommand;