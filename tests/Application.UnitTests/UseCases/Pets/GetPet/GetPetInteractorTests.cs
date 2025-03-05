using AutoMapper;
using MediatR;
using Moq;
using NUnit.Framework;
using VetCheckup.Application.Dtos;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.Pets.GetPet;
using VetCheckup.Domain.Entities;
using VetCheckup.Domain.Enums;

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
            MicrochipId = 123456,
            Name = "Test Pet",
            Owner = new()
            {
                Address = new()
                {
                    Country = "Test Country",
                    PostalCode = "Test Postal Code",
                    State = "Test State",
                    StreetAddress = "Test Street Address",
                    Suburb = "Test Suburb",
                },
                ContactDetails = new(),
                Name = "Test Owner",
            },
            PetId = Guid.NewGuid(),
            Species = "Test Species"
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
                    MicrochipId = 1234,
                    Name = "Test Pet",
                    Sex = Sex.Male,
                    Species = "Test Species",
                    Owner = new()
                    {
                        OwnerId = _petDto.Owner.OwnerId,
                        Address = new()
                        {
                            Country = "Test Country",
                            PostalCode = "Test Postal Code",
                            State = "Test State",
                            StreetAddress = "Test Street Address",
                            Suburb = "Test Suburb",
                        },
                        ContactDetails = new(),
                        Name = "Test Owner",
                    }
                } }.AsQueryable());

            this._mockMapper
                .Setup(mock => mock.Map<PetDto>(It.IsAny<Pet>()))
                .Returns(this._petDto);

            this._interactor = new GetPetInteractor(this._mockDbContext.Object, this._mockMapper.Object);
        }

        #endregion


        #region Interactor Tests

        [Test]
        public async Task GettingPet_PetExists()
        {
            var pet = await this._interactor.Handle(this._request, default);
            
            Assert.That(pet, Is.EqualTo(this._petDto));
        }


        [Test]
        public void GettingPet_ThrowsExceptionWhenOwnerNotFound()
        {
            this._request.PetId = Guid.NewGuid();

            var handleMethod = async () => await this._interactor.Handle(this._request, default);

            Assert.ThrowsAsync<Exception>(() => handleMethod(), "Pet not found.");
        }

        #endregion

    }
}
