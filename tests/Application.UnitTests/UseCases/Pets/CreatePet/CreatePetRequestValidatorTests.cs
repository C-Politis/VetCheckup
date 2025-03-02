using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.Pets.CreatePet;
using VetCheckup.Domain.Entities;
using VetCheckup.Domain.Enums;

namespace VetCheckup.Application.UnitTests.UseCases.Pets.CreatePet
{

    public class CreatePetRequestValidatorTests
    {
        #region Fields

        private readonly Mock<IDbContext> _mockContext = new();
        private readonly Mock<IMapper> _mockMapper = new();

        private readonly IRequestHandler<CreatePetRequest> _createPetInteractor;
        private readonly CreatePetRequest _createPetRequest;
        private readonly Mock<DbSet<Pet>> _mockPetDbSet = new();

        #endregion

        #region Constructors

        public CreatePetRequestValidatorTests()
        {
            _createPetRequest = new CreatePetRequest()
            {
                Name = "New Pet",
                DateOfBirth = new DateTime(2000, 01, 01),
                Species = "Dog",
                MicrochipId = 1234567890,
                Sex = Sex.Male,
                OwnerId = Guid.NewGuid()
            };

            _mockContext
                .Setup(e => e.Get<Owner>())
                .Returns(() => new List<Owner>()
                {
                    new()
                    {
                        OwnerId = _createPetRequest.OwnerId,
                        Address = new Address { Country = string.Empty, PostalCode = string.Empty, State = string.Empty, StreetAddress = string.Empty, Suburb = string.Empty },
                        ContactDetails = new Contact(),
                        Name = "Owner Name"
                    }
                }.AsQueryable());

            _mockMapper
                .Setup(e => e.Map<Pet>(It.IsAny<CreatePetRequest>()))
                .Returns(() => new Pet()
                {
                    Name = _createPetRequest.Name,
                    DateOfBirth = _createPetRequest.DateOfBirth,
                    Species = _createPetRequest.Species,
                    MicrochipId = _createPetRequest.MicrochipId,
                    Owner = new Owner()
                    {
                        Address = new() { Country = string.Empty, PostalCode = string.Empty, State = string.Empty, StreetAddress = string.Empty, Suburb = string.Empty },
                        ContactDetails = new Contact(),
                        Name = "Owner Name"
                    }
                });

            _createPetInteractor = new CreatePetInteractor(this._mockContext.Object, this._mockMapper.Object);
        }

        #endregion

        #region Validator Tests

        [Test]
        public async Task CreatingPet_AddsNewPetToContext()
        {
            await this._createPetInteractor.Handle(_createPetRequest, CancellationToken.None);
            this._mockContext.Verify(mock => mock.Add(It.IsAny<Pet>()), Times.Once);
        }

        #endregion
    }

}
