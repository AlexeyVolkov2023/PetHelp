using PetHelp.Application.Dto;

namespace PetHelp.Application.VolunteerManagement.UpdatePaymentDetail;

public record UpdatePaymentDetailCommand(
    Guid VolunteerId,
    IEnumerable<PaymentDetailDto> PaymentDetails);

