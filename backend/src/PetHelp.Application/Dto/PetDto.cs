using PetHelp.Domain.AnimalManagement.VO;

namespace PetHelp.Application.Dto;

public class PetDto
{
    public Guid Id { get; init; }
    public Guid VolunteerId { get; init; }
    public PetInfoDto PetInfo { get; init; }
    public PetDataDto PetData { get; init; }
    public AddressDto Address { get; init; }
    public string PhoneNumber { get; init; } = string.Empty;
    public DateTime DateOfBirth { get; init; }
    public string Status { get; init; } = String.Empty;
    public DateTime CreatedAt { get; init; }
    public SpeciesBreedDto SpeciesBreed { get; init; }
    public PaymentDetailDto[] PaymentDetails { get; init; } = [];
    public PetFileDto[] Files { get; init; } = null!;
    public int Position { get; init; }
}

public class PetFileDto
{
    public string PathToStorage { get; init; } = string.Empty;
}