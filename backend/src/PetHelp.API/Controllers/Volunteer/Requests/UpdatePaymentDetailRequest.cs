using PetHelp.Application.Dtos;

namespace PetHelp.API.Controllers.Volunteer.Requests;

public record UpdatePaymentDetailRequest(
    IEnumerable<PaymentDetailDto> PaymentDetails);