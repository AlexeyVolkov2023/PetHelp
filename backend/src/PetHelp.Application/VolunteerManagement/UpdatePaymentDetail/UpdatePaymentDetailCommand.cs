using PetHelp.Application.Dtos;

namespace PetHelp.Application.VolunteerManagement.UpdatePaymentDetail;

public record UpdatePaymentDetailCommand(
    Guid VolunteerId,
    IEnumerable<PaymentDetailDto> PaymentDetails);

