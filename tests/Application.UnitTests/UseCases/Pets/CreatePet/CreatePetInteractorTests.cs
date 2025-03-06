using AutoMapper;
using MediatR;
using Moq;
using NUnit.Framework;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.Pets.CreatePet;
using VetCheckup.Domain.Entities;

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
                Name = "New Pet",
                DateOfBirth = new DateTime(2010, 01, 01),
                OwnerId = Guid.NewGuid(),
                Species = "Dog"
            };

            var ownerId = _createPetRequest.OwnerId;

            _mockMapper
                .Setup(e => e.Map<Pet>(It.IsAny<CreatePetRequest>()))
                .Returns(() => new Pet
                {
                    Name = _createPetRequest.Name,
                    DateOfBirth = _createPetRequest.DateOfBirth,
                    Owner = new Owner
                    {
                        OwnerId = ownerId,
                        Address = new Address
                        {
                            Country = "Country",
                            PostalCode = "PostalCode",
                            State = "State",
                            StreetAddress = "StreetAddress",
                            Suburb = "Suburb"
                        },
                        ContactDetails = new Contact(),
                        Name = "Owner Name"
                    },
                    Species = _createPetRequest.Species
                });

            _mockContext
                .Setup(e => e.Get<Owner>())
                .Returns(new List<Owner>
                {
                    new Owner
                    {
                        OwnerId = ownerId,
                        Address = new Address
                        {
                            Country = "Country",
                            PostalCode = "PostalCode",
                            State = "State",
                            StreetAddress = "StreetAddress",
                            Suburb = "Suburb"
                        },
                        ContactDetails = new Contact(),
                        Name = "Owner Name"
                    }
                }.AsQueryable());

            _createPetInteractor = new CreatePetInteractor(_mockContext.Object, _mockMapper.Object);
        } 

        #endregion


        #region Interactor Tests

        [Test]
        public async Task CreatingPet_AddsNewPetToContext()
        {
            await _createPetInteractor.Handle(_createPetRequest, default);
            _mockContext.Verify(mock => mock.Add(It.IsAny<Pet>()), Times.Once);
        }

        [Test]
        public void CreatingPet_ThrowsExceptionWhenOwnerNotFound()
        {
            _mockContext
                .Setup(e => e.Get<Owner>())
                .Returns(new List<Owner>().AsQueryable());

            var exception = Assert.Throws<Exception>(()
                => _createPetInteractor.Handle(_createPetRequest, default));

            Assert.That(exception.Message, Is.EqualTo("Owner not found"));
        }

        #endregion

    }

}
