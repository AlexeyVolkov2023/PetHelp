using FluentAssertions;
using PetHelp.Domain.AnimalManagement.AggregateRoot;
using PetHelp.Domain.AnimalManagement.Entities;
using PetHelp.Domain.AnimalManagement.ID;
using PetHelp.Domain.AnimalManagement.VO;
using PetHelp.Domain.Shared;
using PetHelp.Domain.SpeciesManagement.VO;

namespace PetHelp.Test;

public class VolunteerTest
{
    [Fact]
    public void Add_Pet_First_Return_Success_Result()
    {
        var volunteerId = VolunteerId.NewVolunteerId();
        var fullName = FullName.Create("Тест", "Тестов", "Тестович").Value;
        var email = Email.Create("test@example.com").Value;
        var description = Description.Create("Тестовый волонтер").Value;
        var experience = ExperienceInYears.Create(1).Value;
        var phone = PhoneNumber.Create("+79160000000").Value;
        var paymentDetails = new List<PaymentDetail>();
        var socialNetworks = new List<SocialNetwork>();

        var volunteer = new Volunteer(volunteerId, fullName, email, description, experience, phone, paymentDetails,
            socialNetworks);

        var petId = PetId.NewPetId();
        var petInfo = PetInfo.Create("питомец", "Тестовое описание").Value;
        var petData = PetData.Create("test", "test", 1, 1, true, true).Value;
        var address = Address.Create("123", "121", "121", "123", "123", 125).Value;
        var phoneNumber = PhoneNumber.Create("+79160000050").Value;
        var status = PetStatus.Create("NeedsHelp").Value;
        var dateOfBirth = DateOfBirth.Create(new DateTime(2020, 1, 1)).Value;
        var speciesBreed = PetSpeciesBreed.Create(Guid.NewGuid(), Guid.NewGuid()).Value;
        var files = new List<PetFile>();
        var details = new List<PaymentDetail>();

        var pet = new Pet(petId, petInfo, petData, address, phoneNumber, status, dateOfBirth, speciesBreed, files,
            details);

        var result = volunteer.AddPet(pet);
        var addedPetResult = volunteer.GetPetById(volunteer.Pets[0].Id);


        Assert.True(result.IsSuccess);
        Assert.True(addedPetResult.IsSuccess);
        var addedPet = addedPetResult.Value;
        Assert.Equal(addedPet.Id, volunteer.Pets[0].Id);
        Assert.Equal(addedPet.Position, Position.First);
    }

    [Fact]
    public void Add_Pet_With_Other_Pet_Return_Success_Result()
    {
        const int PET_COUNT = 5;
        var volunteerId = VolunteerId.NewVolunteerId();
        var fullName = FullName.Create("Тест", "Тестов", "Тестович").Value;
        var email = Email.Create("test@example.com").Value;
        var description = Description.Create("Тестовый волонтер").Value;
        var experience = ExperienceInYears.Create(1).Value;
        var phone = PhoneNumber.Create("+79160000000").Value;
        var paymentDetails = new List<PaymentDetail>();
        var socialNetworks = new List<SocialNetwork>();

        var volunteer = new Volunteer(volunteerId, fullName, email, description, experience, phone, paymentDetails,
            socialNetworks);

        var petId = PetId.NewPetId();
        var petInfo = PetInfo.Create("питомец", "Тестовое описание").Value;
        var petData = PetData.Create("test", "test", 1, 1, true, true).Value;
        var address = Address.Create("123", "121", "121", "123", "123", 125).Value;
        var phoneNumber = PhoneNumber.Create("+79160000050").Value;
        var status = PetStatus.Create("NeedsHelp").Value;
        var dateOfBirth = DateOfBirth.Create(new DateTime(2020, 1, 1)).Value;
        var speciesBreed = PetSpeciesBreed.Create(Guid.NewGuid(), Guid.NewGuid()).Value;
        var files = new List<PetFile>();
        var details = new List<PaymentDetail>();

        var pets = Enumerable.Range(1, PET_COUNT).Select(_ =>
            new Pet(PetId.NewPetId(), petInfo, petData, address, phoneNumber, status, dateOfBirth, speciesBreed, files,
                details));

        foreach (var petAdd in pets)
            volunteer.AddPet(petAdd);

        var pet = new Pet(petId, petInfo, petData, address, phoneNumber, status, dateOfBirth, speciesBreed, files,
            details);

        var result = volunteer.AddPet(pet);

        var addedPetResult = volunteer.GetPetById(petId);

        var position = Position.Create(PET_COUNT + 1);

        result.IsSuccess.Should().BeTrue();
        addedPetResult.IsSuccess.Should().BeTrue();
        addedPetResult.Value.Id.Should().Be(pet.Id);
        addedPetResult.Value.Position.Should().Be(position.Value);
    }

    [Fact]
    public void Move_Pet_Should_Not_Move_When_Pet_Already_At_New_Position()
    {
        //Arrange
        const int petsCount = 5;
        
        var volunteer = MakeVolunteerWithPets(petsCount);
        
        var secondPosition = Position.Create(2).Value;

        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];
        
        //Act
        var result = volunteer.MovePet(secondPet, secondPosition);
        
        //Assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(1);
        secondPet.Position.Value.Should().Be(2);
        thirdPet.Position.Value.Should().Be(3);
        fourthPet.Position.Value.Should().Be(4);
        fifthPet.Position.Value.Should().Be(5);
    }
    
    [Fact]
    public void Move_Pet_Should_Move_Other_Pet_Forward_When_New_Position_Is_Lower()
    {
        //Arrange
        const int petsCount = 5;
        
        var volunteer = MakeVolunteerWithPets(petsCount);
        
        var secondPosition = Position.Create(2).Value;

        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];
        
        //Act
        var result = volunteer.MovePet(fourthPet, secondPosition);
        
        //Assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(1);
        secondPet.Position.Value.Should().Be(3);
        thirdPet.Position.Value.Should().Be(4);
        fourthPet.Position.Value.Should().Be(2);
        fifthPet.Position.Value.Should().Be(5);
    }
    
    [Fact]
    public void Move_Pet_Should_Move_Other_Pet_Back_When_New_Position_Is_Greater()
    {
        //Arrange
        const int petsCount = 5;
        
        var volunteer = MakeVolunteerWithPets(petsCount);
        
        var fourthPosition = Position.Create(4).Value;

        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];
        
        //Act
        var result = volunteer.MovePet(secondPet, fourthPosition);
        
        //Assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(1);
        secondPet.Position.Value.Should().Be(4);
        thirdPet.Position.Value.Should().Be(2);
        fourthPet.Position.Value.Should().Be(3);
        fifthPet.Position.Value.Should().Be(5);
    }
    
    [Fact]
    public void Move_Pet_Should_Move_Other_Pet_Forward_When_New_Position_Is_First()
    {
        //Arrange
        const int petsCount = 5;
        
        var volunteer = MakeVolunteerWithPets(petsCount);
        
        var firstPosition = Position.Create(1).Value;

        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];
        
        //Act
        var result = volunteer.MovePet(fifthPet, firstPosition);
        
        //Assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(2);
        secondPet.Position.Value.Should().Be(3);
        thirdPet.Position.Value.Should().Be(4);
        fourthPet.Position.Value.Should().Be(5);
        fifthPet.Position.Value.Should().Be(1);
    }
    
    [Fact]
    public void Move_Pet_Should_Move_Other_Pet_Back_When_New_Position_Is_Last()
    {
        //Arrange
        const int petsCount = 5;
        
        var volunteer = MakeVolunteerWithPets(petsCount);
        
        var fifthPosition = Position.Create(5).Value;

        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];
        
        //Act
        var result = volunteer.MovePet(firstPet, fifthPosition);
        
        //Assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(5);
        secondPet.Position.Value.Should().Be(1);
        thirdPet.Position.Value.Should().Be(2);
        fourthPet.Position.Value.Should().Be(3);
        fifthPet.Position.Value.Should().Be(4);
    }

    private Volunteer MakeVolunteerWithPets(int petsCount)
    {
        var volunteerId = VolunteerId.NewVolunteerId();
        var fullName = FullName.Create("Тест", "Тестов", "Тестович").Value;
        var email = Email.Create("test@example.com").Value;
        var description = Description.Create("Тестовый волонтер").Value;
        var experience = ExperienceInYears.Create(1).Value;
        var phone = PhoneNumber.Create("+79160000000").Value;
        var paymentDetails = new List<PaymentDetail>();
        var socialNetworks = new List<SocialNetwork>();

        var volunteer = new Volunteer(volunteerId, fullName, email, description, experience, phone, paymentDetails,
            socialNetworks);

        var petId = PetId.NewPetId();
        var petInfo = PetInfo.Create("питомец", "Тестовое описание").Value;
        var petData = PetData.Create("test", "test", 1, 1, true, true).Value;
        var address = Address.Create("123", "121", "121", "123", "123", 125).Value;
        var phoneNumber = PhoneNumber.Create("+79160000050").Value;
        var status = PetStatus.Create("NeedsHelp").Value;
        var dateOfBirth = DateOfBirth.Create(new DateTime(2020, 1, 1)).Value;
        var speciesBreed = PetSpeciesBreed.Create(Guid.NewGuid(), Guid.NewGuid()).Value;
        var files = new List<PetFile>();
        var details = new List<PaymentDetail>();

        var pets = Enumerable.Range(1, petsCount).Select(_ =>
            new Pet(PetId.NewPetId(), petInfo, petData, address, phoneNumber, status, dateOfBirth, speciesBreed, files,
                details));

        foreach (var petAdd in pets)
            volunteer.AddPet(petAdd);

        return volunteer;
    }
}