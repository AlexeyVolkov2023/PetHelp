using PetHelp.Application.Dto;

namespace PetHelp.API.Controllers.Volunteer.Requests;

public record UpdatePaymentDetailRequest(
    IEnumerable<PaymentDetailDto> PaymentDetails);