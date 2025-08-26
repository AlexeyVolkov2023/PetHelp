using PetHelp.Application.Dto;

namespace PetHelp.API.Controllers.Volunteer.Requests;

public record AddPetRequest(
    PetInfoDto PetInfoDto,
    PetDataDto PetDataDto,
    AddressDto AddressDto,
    string PhoneNumber,
    string Status,
    DateTime DateOfBirth,
    Guid SpeciesId,
    Guid BreedId,
    IEnumerable<PaymentDetailDto> PaymentDetails);