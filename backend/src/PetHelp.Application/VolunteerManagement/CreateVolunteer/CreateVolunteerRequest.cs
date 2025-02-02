namespace PetHelp.Application.VolunteerManagement.CreateVolunteer;

public record CreateVolunteerRequest(
    FullNameFto FullName,
    string Email,
    string Description,
    int ExperienceInYears,
    string PhoneNumber,
    IEnumerable<CreatePaymentDetail> Details,
    IEnumerable<CreateSocialNetwork> Networks);
public record CreateSocialNetwork(string Name, string Link);
public record CreatePaymentDetail(string Title, string Description);
public record FullNameFto(string Name, string Surname, string? Patronymik);