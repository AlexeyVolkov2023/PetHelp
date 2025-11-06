using PetHelp.Application.Abstraction;
using PetHelp.Application.Dto;

namespace PetHelp.Application.PetManagement.Commands.UpdatePaymentDetail;

public record UpdatePaymentDetailCommand(
    Guid VolunteerId,
    IEnumerable<PaymentDetailDto> PaymentDetails) : ICommand;

