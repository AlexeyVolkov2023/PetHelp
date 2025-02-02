namespace PetHelp.Application.VolunteerManagement.CreateVolunteer;

public record CreateVolunteerRequest(
    string FullName,
    string Email,
    string Description,
    int ExperienceInYears,
    string PhoneNumber,
    IEnumerable<CreatePaymentDetail> Details,
    IEnumerable<CreateSocialNetwork> Networks);
public record CreateSocialNetwork(string Name, string Link);
public record CreatePaymentDetail (string Title, string Description);
    