using PetHelp.Application.Dto;

namespace PetHelp.API.Controllers.Volunteer.Requests;

public record AddPetRequest(
    PetInfoDto PetInfoDto,
    PetDataDto PetDataDto,
    AddressDto AddressDto,
    string PhoneNumber,
    string Status,
    DateTime DateOfBirth,
    string Species,
    string Breed,
    IEnumerable<PaymentDetailDto> PaymentDetails);
