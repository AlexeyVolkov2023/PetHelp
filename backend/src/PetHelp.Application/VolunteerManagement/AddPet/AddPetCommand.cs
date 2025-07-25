using PetHelp.Application.Dto;

namespace PetHelp.Application.VolunteerManagement.AddPet;

public record AddPetCommand(
    Guid VolunteerId,
    PetInfoDto PetInfo,
    PetDataDto PetData,
    AddressDto Address,
    string PhoneNumber,
    string Status,
    DateTime DateOfBirth,
    Guid SpeciesId,
    Guid BreedId,
    IEnumerable<CreateFileDto> Files,
    IEnumerable<PaymentDetailDto> PaymentDetails);