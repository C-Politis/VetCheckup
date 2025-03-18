using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Moq;
using VetCheckup.Application.Common.Enums;
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
                FirstName = "New Owner",
                MiddleName = "New Owner",
                LastName = "New Owner",
                Title = Title.Dr,
                Suffix = Suffix.III,
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
                            ContactDetails = new Contact()
                            {
                                Email = string.Empty,
                                Mobile = string.Empty
                            },
                            OwnerId = _updateOwnerRequest.OwnerId,
                            FirstName = "Old Owner",
                            MiddleName = "Old Owner",
                            LastName = "Old Owner",
                            Title = Title.Mrs,
                            Suffix = Suffix.Jr,
                            DateOfBirth = new DateTime(2010, 01, 01),
                            Pets = new List<Pet>()
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
                            ContactDetails = new Contact() 
                            { 
                                Email = string.Empty,
                                Mobile = string.Empty
                            },
                            FirstName = "Owner Name",
                            MiddleName = "Owner Name",
                            LastName = "Owner Name",
                            Title = Title.Miss,
                            Suffix = Suffix.Esq,
                            Pets = new List<Pet>()
                        } }.AsQueryable());

            _mockMapper
                .Setup(e => e.Map(It.IsAny<UpdateOwnerRequest>(), It.IsAny<Owner>()))
                .Callback<UpdateOwnerRequest, Owner>((request, owner) =>
                {
                    owner.FirstName = request.FirstName ?? owner.FirstName;
                    owner.MiddleName = request.MiddleName ?? owner.MiddleName;
                    owner.LastName = request.LastName ?? owner.LastName;
                    owner.Title = request.Title ?? owner.Title;
                    owner.Suffix = request.Suffix ?? owner.Suffix;
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
                FirstName = "Non Existent Owner",
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
