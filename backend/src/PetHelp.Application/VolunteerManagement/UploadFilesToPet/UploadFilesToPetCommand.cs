using PetHelp.Application.Dto;

namespace PetHelp.Application.VolunteerManagement.UploadFilesToPet;

public record UploadFilesToPetCommand(Guid VolunteerId, Guid PetId, IEnumerable<UploadFileDto> Files);