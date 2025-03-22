using AutoMapper;
using MediatR;
using Moq;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.Vets.UpdateVet;
using VetCheckup.Domain.Entities;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Vets.UpdateVet
{
    public class UpdateVetInteractorTests
    {

        #region Fields

        private readonly Mock<IDbContext> _mockContext = new();
        private readonly Mock<IMapper> _mockMapper = new();

        private readonly IRequestHandler<UpdateVetRequest> _updateVetInteractor;
        private readonly UpdateVetRequest _updateVetRequest;

        #endregion

        #region Constructors

        public UpdateVetInteractorTests()
        {
            _updateVetRequest = new UpdateVetRequest()
            {
                VetId = Guid.NewGuid(),
                Address = new(),
                ContactDetails = new(),
            };

            _mockContext
                .Setup(e => e.Get<Vet>())
                .Returns(new List<Vet> { new Vet
                    {
                        VetId = _updateVetRequest.VetId,
                        Address = new()
                        {
                            Country = "Country",
                            PostalCode = "PostalCode",
                            State = "State",
                            StreetAddress = "StreetAddress",
                            Suburb = "Suburb"
                        },
                        ContactDetails = new(),
                        Name = "Old Name",
                        DateOfBirth = DateTime.MinValue
                    }
                }.AsQueryable());

            _mockMapper
                .Setup(e => e.Map(It.IsAny<UpdateVetRequest>(), It.IsAny<Vet>()))
                .Callback<UpdateVetRequest, Vet>((request, vet) =>
                {
                    vet.Name = request.Name ?? vet.Name;
                    vet.DateOfBirth = request.DateOfBirth ?? vet.DateOfBirth;
                });

            _updateVetInteractor = new UpdateVetInteractor(this._mockContext.Object, this._mockMapper.Object);
        }

        #endregion

        #region Interactor Tests

        [Fact]
        public async Task UpdatingVet_UpdatesExistingPetToContextAsync()
        {
            // Act
            await _updateVetInteractor.Handle(_updateVetRequest, CancellationToken.None);

            // Assert
            _mockContext.Verify(mock => mock.Get<Vet>(), Times.Once);
            _mockMapper.Verify(mock => mock.Map(It.IsAny<UpdateVetRequest>(), It.IsAny<Vet>()), Times.Once);
        }

        [Fact]
        public async Task UpdatingVet_ThrowsExceptionWhenVetNotFound()
        {
            // Arrange
            var nonExistentVetRequest = new UpdateVetRequest
            {
                VetId = Guid.NewGuid(),
                Name = "Non Existent Vet",
                Address = new(),
                ContactDetails = new(),
                DateOfBirth = new DateTime(2010, 01, 01)
            };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
                await _updateVetInteractor.Handle(nonExistentVetRequest, CancellationToken.None));
        }

        #endregion

    }
}
