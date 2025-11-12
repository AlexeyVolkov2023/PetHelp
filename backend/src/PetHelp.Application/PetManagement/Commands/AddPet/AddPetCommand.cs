using PetHelp.Application.Abstraction;
using PetHelp.Application.Dto;

namespace PetHelp.Application.PetManagement.Commands.AddPet;

public record AddPetCommand(
    Guid VolunteerId,
    PetInfoDto PetInfo,
    PetDataDto PetData,
    AddressDto Address,
    string PhoneNumber,
    string Status,
    DateTime DateOfBirth,
    string Species,
    string Breed,
    IEnumerable<PaymentDetailDto> PaymentDetails): ICommand;