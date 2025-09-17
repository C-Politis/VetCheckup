using AutoMapper;
using MediatR;
using Moq;
using VetCheckup.Application.Dtos;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.Pets.GetPet;
using VetCheckup.Domain.Entities;
using VetCheckup.Domain.Enums;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Pets.GetPet
{
    public class GetPetInteractorTests
    {

        #region Saurons

        private readonly Mock<IDbContext> _mockDbContext = new();
        private readonly Mock<IMapper> _mockMapper = new();

        private readonly GetPetRequest _request = new()
        {
            PetId = Guid.NewGuid()
        };
        private readonly IRequestHandler<GetPetRequest, PetDto> _interactor;
        private readonly PetDto _petDto = new()
        {
            DateOfBirth = DateTime.Now,
            MicrochipId = "123456",
            Name = "Test Pet",
            Owner = new()
            {
                OwnerId = Guid.NewGuid(),
                Address = new()
                {
                    AddressId = Guid.NewGuid(),
                    Country = "Test Country",
                    PostalCode = "Test Postal Code",
                    State = "Test State",
                    StreetAddress = "Test Street Address",
                    Suburb = "Test Suburb",
                },
                ContactDetails = new()
                {
                    ContactId = Guid.NewGuid(),
                    Email = string.Empty,
                    Mobile = string.Empty
                },
                FirstName = "Test",
                LastName = "Owner",
                MiddleName = "Middle",
                Suffix = Suffix.Esq,
                Title = Title.Dr,
                Pets = new List<PetDto>(),
                DateOfBirth = DateTime.MinValue
            },
            PetId = Guid.NewGuid(),
            Species = "Test Species",
            Sex = Sex.Female
        };

        #endregion

        #region Constructors

        public GetPetInteractorTests()
        {
            this._mockDbContext
                .Setup(mock => mock.Get<Pet>())
                .Returns(new[] { new Pet()
                {
                    PetId = this._request.PetId,
                    DateOfBirth = DateTime.Now,
                    MicrochipId = "123456",
                    Name = "Test Pet",
                    Sex = Sex.Male,
                    Species = "Test Species",
                    Owner = new()
                    {
                        OwnerId = _petDto.Owner.OwnerId,
                        Address = new()
                        {
                            AddressId = _petDto.Owner.Address.AddressId,
                            Country = "Test Country",
                            PostalCode = "Test Postal Code",
                            State = "Test State",
                            StreetAddress = "Test Street Address",
                            Suburb = "Test Suburb",
                        },
                        ContactDetails = new()
                        {
                            ContactId = _petDto.Owner.ContactDetails.ContactId,
                            Email = string.Empty,
                            Mobile = string.Empty
                        },
                        User = new()
                        {
                            UserId = Guid.NewGuid(),
                            UserName = "MyUser",
                            Password = "Password",
                            UserType = UserType.OrganisationManager
                        },
                        FirstName = "Test",
                        LastName = "Owner",
                        MiddleName = "Middle",
                        Suffix = Suffix.Esq,
                        Title = Title.Dr,
                        Pets = new List<Pet>(),
                        DateOfBirth = DateTime.MinValue
                    }
                } }.AsQueryable());

            this._mockMapper
                .Setup(mock => mock.Map<PetDto>(It.IsAny<Pet>()))
                .Returns(this._petDto);

            this._interactor = new GetPetInteractor(this._mockDbContext.Object, this._mockMapper.Object);
        }

        #endregion


        #region Interactor Tests

        [Fact]
        public async Task GettingPet_PetExists()
        {
            //Act
            var pet = await this._interactor.Handle(this._request, default);

            //Assert
            Assert.Equal(this._petDto, pet);
        }


        [Fact]
        public async Task GettingPet_ThrowsExceptionWhenOwnerNotFound()
        {
            //Arrange
            this._request.PetId = Guid.NewGuid();

            //Act
            var handleMethod = async () => await this._interactor.Handle(this._request, default);

            //Assert
            await Assert.ThrowsAsync<Exception>(handleMethod);
        }

        #endregion

    }
}
