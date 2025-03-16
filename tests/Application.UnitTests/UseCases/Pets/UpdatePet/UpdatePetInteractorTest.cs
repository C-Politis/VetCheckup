
using System.Threading;
using AutoMapper;
using MediatR;
using Moq;
using VetCheckup.Application.Common.Enums;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.Pets.UpdatePet;
using VetCheckup.Domain.Entities;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Pets.UpdatePet
{
    public class UpdatePetInteractorTest
    {
        #region Fields

        private readonly Mock<IDbContext> _mockContext = new();
        private readonly Mock<IMapper> _mockMapper = new();

        private readonly IRequestHandler<UpdatePetRequest> _updatePetInteractor;
        private readonly UpdatePetRequest _updatePetRequest;

        #endregion

        #region Constructor

        public UpdatePetInteractorTest()
        {
            _updatePetRequest = new UpdatePetRequest
            {
                PetId = Guid.NewGuid(),
                Name = "New Pet",
                DateOfBirth = new DateTime(2010, 01, 01),
                OwnerId = Guid.NewGuid(),
                Species = "Dog"
            };

            _mockContext
                .Setup(e => e.Get<Pet>())
                .Returns(new List<Pet> { new Pet
                        {
                            PetId = _updatePetRequest.PetId,
                            Name = "Old Pet",
                            DateOfBirth = new DateTime(2010, 01, 01),
                            Owner = new Owner
                            {
                                OwnerId = _updatePetRequest.OwnerId ?? Guid.Empty,
                                Address = new Address
                                {
                                    Country = "Country",
                                    PostalCode = "PostalCode",
                                    State = "State",
                                    StreetAddress = "StreetAddress",
                                    Suburb = "Suburb"
                                },
                                ContactDetails = new Contact(),
                                FirstName = "Test",
                                LastName = "Owner",
                                MiddleName = "Middle",
                                Suffix = Suffix.Esq,
                                Title = Title.Dr,
                            },
                            Species = "Dog"
                        }
            }.AsQueryable());


            _mockContext
                .Setup(e => e.Get<Owner>())
                .Returns(new List<Owner> { new Owner
                        {
                            OwnerId = _updatePetRequest.OwnerId ?? Guid.Empty,
                            Address = new Address
                            {
                                Country = "Country",
                                PostalCode = "PostalCode",
                                State = "State",
                                StreetAddress = "StreetAddress",
                                Suburb = "Suburb"
                            },
                            ContactDetails = new Contact(),
                            FirstName = "Test",
                            LastName = "Owner",
                            MiddleName = "Middle",
                            Suffix = Suffix.Esq,
                            Title = Title.Dr,
                        } }.AsQueryable());

            _mockMapper
                .Setup(e => e.Map(It.IsAny<UpdatePetRequest>(), It.IsAny<Pet>()))
                .Callback<UpdatePetRequest, Pet>((request, pet) =>
                {
                    pet.Name = request.Name ?? pet.Name;
                    pet.DateOfBirth = request.DateOfBirth;
                    pet.Species = request.Species ?? pet.Species;
                });

            _updatePetInteractor = new UpdatePetInteractor(this._mockContext.Object, this._mockMapper.Object);

        }

        #endregion

        #region Interactor Tests

        [Fact]
        public async Task UpdatingPet_UpdatesExistingPetToContextAsync()
        {
            // Act
            await _updatePetInteractor.Handle(_updatePetRequest, CancellationToken.None);

            // Assert
            _mockContext.Verify(mock => mock.Get<Pet>(), Times.Once);
            _mockContext.Verify(mock => mock.Get<Owner>(), Times.Once);
            _mockMapper.Verify(mock => mock.Map(It.IsAny<UpdatePetRequest>(), It.IsAny<Pet>()), Times.Once);
        }

        [Fact]
        public async Task UpdatingPet_ThrowsExceptionWhenOwnerNotFound()
        {

            // Arrange
            var nonExistentOwnerRequest = new UpdatePetRequest
            {
                PetId = _updatePetRequest.PetId,
                Name = "Non Existent Owner",
                DateOfBirth = new DateTime(2010, 01, 01),
                OwnerId = Guid.NewGuid(),
                Species = "Dog"
            };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
                await _updatePetInteractor.Handle(nonExistentOwnerRequest, CancellationToken.None));

        }

        [Fact]
        public async Task UpdatingPet_ThrowsExceptionWhenPetNotFound()
        {
            // Arrange
            var nonExistentPetRequest = new UpdatePetRequest
            {
                PetId = Guid.NewGuid(),
                Name = "Non Existent Pet",
                DateOfBirth = new DateTime(2010, 01, 01),
                OwnerId = _updatePetRequest.OwnerId,
                Species = "Dog"
            };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
                await _updatePetInteractor.Handle(nonExistentPetRequest, CancellationToken.None));
        }

        #endregion
    }

}
