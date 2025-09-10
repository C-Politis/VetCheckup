using AutoMapper;
using MediatR;
using Moq;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.Pets.CreatePet;
using VetCheckup.Domain.Entities;
using VetCheckup.Domain.Enums;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Pets.CreatePet
{

    public class CreatePetInteractorTests
    {

        #region Fields

        private readonly Mock<IDbContext> _mockContext = new();
        private readonly Mock<IMapper> _mockMapper = new();

        private readonly IRequestHandler<CreatePetRequest> _createPetInteractor;
        private readonly CreatePetRequest _createPetRequest;

        #endregion

        #region Constructor

        public CreatePetInteractorTests()
        {
            _createPetRequest = new CreatePetRequest
            {
                MicrochipId = "12345",
                Name = "New Pet",
                DateOfBirth = new DateTime(2010, 01, 01),
                OwnerId = Guid.NewGuid(),
                Species = "Dog",
                Sex = Sex.Male
            };

            Guid AddressId = Guid.NewGuid();
            Guid ContactId = Guid.NewGuid();
            Guid UserId = Guid.NewGuid();

            _mockMapper
                .Setup(e => e.Map<Pet>(It.IsAny<CreatePetRequest>()))
                .Returns(() => new Pet
                {
                    PetId = Guid.NewGuid(),
                    MicrochipId = "12345",
                    Name = _createPetRequest.Name,
                    DateOfBirth = _createPetRequest.DateOfBirth,
                    Owner = new Owner
                    {
                        OwnerId = _createPetRequest.OwnerId,
                        Address = new Address
                        {
                            AddressId = AddressId,
                            Country = "Country",
                            PostalCode = "PostalCode",
                            State = "State",
                            StreetAddress = "StreetAddress",
                            Suburb = "Suburb"
                        },
                        ContactDetails = new Contact()
                        { 
                            ContactId = ContactId,
                            Email = string.Empty,
                            Mobile = string.Empty
                        },
                        User = new()
                        {
                            UserId = UserId,
                            UserName = "MyUser",
                            Password = "Password",
                            UserType = UserType.Organisation
                        },
                        FirstName = "Test",
                        LastName = "Owner",
                        MiddleName = "Middle",
                        Suffix = Suffix.Esq,
                        Title = Title.Dr,
                        Pets = new List<Pet>(),
                        DateOfBirth = DateTime.MinValue
                    },
                    Species = _createPetRequest.Species,
                    Sex = Sex.Male
                });

            _mockContext
                .Setup(e => e.Get<Owner>())
                .Returns(new List<Owner>
                {
                    new Owner
                    {
                        OwnerId = _createPetRequest.OwnerId,
                        Address = new Address
                        {
                            AddressId = AddressId,
                            Country = "Country",
                            PostalCode = "PostalCode",
                            State = "State",
                            StreetAddress = "StreetAddress",
                            Suburb = "Suburb"
                        },
                        ContactDetails = new Contact()
                        {
                            ContactId = ContactId,
                            Email = string.Empty,
                            Mobile = string.Empty
                        },
                        User = new()
                        {
                            UserId = UserId,
                            UserName = "MyUser",
                            Password = "Password",
                            UserType = UserType.Organisation
                        },
                        FirstName = "Test",
                        LastName = "Owner",
                        MiddleName = "Middle",
                        Suffix = Suffix.Esq,
                        Title = Title.Dr,
                        Pets = new List<Pet>(),
                        DateOfBirth = DateTime.MinValue
                    }
                }.AsQueryable());

            _createPetInteractor = new CreatePetInteractor(_mockContext.Object, _mockMapper.Object);
        }

        #endregion


        #region Interactor Tests

        [Fact]
        public async Task CreatingPet_AddsNewPetToContext()
        {
            // Act
            await _createPetInteractor.Handle(_createPetRequest, default);

            // Assert
            _mockContext.Verify(mock => mock.Add(It.IsAny<Pet>()), Times.Once);
        }

        [Fact]
        public async Task CreatingPet_ThrowsExceptionWhenOwnerNotFound()
        {
            // Arrange
            _mockContext
                .Setup(e => e.Get<Owner>())
                .Returns(new List<Owner>().AsQueryable());

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(()
                => _createPetInteractor.Handle(_createPetRequest, default));

            // Assert
            Assert.Equal("Owner not found", exception.Message);
        }


        #endregion

    }

}
