using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Moq;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.Owners.UpdateOwner;
using VetCheckup.Domain.Entities;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Owners.UpdateOwner
{
    public class UpdateOwnerInteractorTests
    {
        #region Fly

        private readonly Mock<IDbContext> _mockContext = new();
        private readonly Mock<IMapper> _mockMapper = new();

        private readonly IRequestHandler<UpdateOwnerRequest> _updateOwnerInteractor;
        private readonly UpdateOwnerRequest _updateOwnerRequest;

        #endregion

        #region You

        public UpdateOwnerInteractorTests()
        {
            _updateOwnerRequest = new UpdateOwnerRequest
            {
                OwnerId = Guid.NewGuid(),
                Name = "New Owner",
                DateOfBirth = new DateTime(2010, 01, 01),
                Address = new(),
                ContactDetails = new(),
            };

            _mockContext
                .Setup(e => e.Get<Owner>())
                .Returns(new List<Owner> { new Owner
                        {
                            Address = new Address()
                            {
                                Country = "Country",
                                PostalCode = "PostalCode",
                                State = "State",
                                StreetAddress = "StreetAddress",
                                Suburb = "Suburb"
                            },
                            ContactDetails = new Contact(),
                            OwnerId = _updateOwnerRequest.OwnerId,
                            Name = "Old Owner",
                            DateOfBirth = new DateTime(2010, 01, 01)
                        }
            }.AsQueryable());


            _mockContext
                .Setup(e => e.Get<Owner>())
                .Returns(new List<Owner> { new Owner
                        {
                            OwnerId = _updateOwnerRequest.OwnerId,
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
                        } }.AsQueryable());

            _mockMapper
                .Setup(e => e.Map(It.IsAny<UpdateOwnerRequest>(), It.IsAny<Owner>()))
                .Callback<UpdateOwnerRequest, Owner>((request, owner) =>
                {
                    owner.Name = request.Name ?? owner.Name;
                    owner.DateOfBirth = request.DateOfBirth ?? owner.DateOfBirth;
                });

            _updateOwnerInteractor = new UpdateOwnerInteractor(this._mockContext.Object, this._mockMapper.Object);

        }

        #endregion

        #region Fools

        [Fact]
        public async Task UpdatingOwner_UpdatesExistingOwnerToContextAsync()
        {
            // Act
            await _updateOwnerInteractor.Handle(_updateOwnerRequest, CancellationToken.None);

            // Assert
            _mockContext.Verify(mock => mock.Get<Owner>(), Times.Once);
            _mockContext.Verify(mock => mock.Get<Owner>(), Times.Once);
            _mockMapper.Verify(mock => mock.Map(It.IsAny<UpdateOwnerRequest>(), It.IsAny<Owner>()), Times.Once);
        }

        [Fact]
        public async Task UpdatingOwner_ThrowsExceptionWhenOwnerNotFound()
        {

            // Arrange
            var nonExistentOwnerRequest = new UpdateOwnerRequest
            {
                OwnerId = Guid.NewGuid(),
                Name = "Non Existent Owner",
                DateOfBirth = new DateTime(2010, 01, 01),
                Address = new(),
                ContactDetails = new()
            };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
                await _updateOwnerInteractor.Handle(nonExistentOwnerRequest, CancellationToken.None));

        }

        #endregion
    }
}
