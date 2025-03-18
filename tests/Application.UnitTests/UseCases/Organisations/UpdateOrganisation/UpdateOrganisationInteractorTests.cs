using AutoMapper;
using MediatR;
using Moq;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.Organisations.UpdateOrganisation;
using VetCheckup.Domain.Entities;
using VetCheckup.Domain.Enums;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Organisations.UpdateOrganisation
{
    public class UpdateOrganisationInteractorTests
    {

        #region We've had one yes,

        private readonly Mock<IDbContext> _mockContext = new();
        private readonly Mock<IMapper> _mockMapper = new();

        private readonly IRequestHandler<UpdateOrganisationRequest> _updateOrganisationInteractor;
        private readonly UpdateOrganisationRequest _updateOrganisationRequest;

        #endregion

        #region Constructors

        public UpdateOrganisationInteractorTests()
        {
            _updateOrganisationRequest = new UpdateOrganisationRequest()
            {
                OrganisationId = Guid.NewGuid(),
                Abn = "51824753556",
                Address = new(),
                ContactDetails = new(),
                Name = "New Name",
                OrganisationType = OrganisationType.Shelter
            };

            _mockContext
                .Setup(e => e.Get<Organisation>())
                .Returns(new List<Organisation> { new Organisation
                        {
                            OrganisationId = _updateOrganisationRequest.OrganisationId,
                            Abn = "02110000000",
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
                             Name = "Old name",
                             OrganisationType = OrganisationType.Clinic
                        }
                }.AsQueryable());

            _mockMapper
                .Setup(e => e.Map(It.IsAny<UpdateOrganisationRequest>(), It.IsAny<Organisation>()))
                .Callback<UpdateOrganisationRequest, Organisation>((request, organisation) =>
                {
                    organisation.Abn = request.Abn ?? organisation.Abn;
                    organisation.Name = request.Name ?? organisation.Name;
                    organisation.OrganisationType = request.OrganisationType ?? organisation.OrganisationType;
                });

            _updateOrganisationInteractor = new UpdateOrganisationInteractor(this._mockContext.Object, this._mockMapper.Object);
        }

        #endregion

        #region Handle tests

        [Fact]
        public async Task UpdatingOrganisation_UpdatesExistingOrganisationToContextAsync()
        {
            // Act
            await _updateOrganisationInteractor.Handle(_updateOrganisationRequest, CancellationToken.None);

            // Assert
            _mockContext.Verify(mock => mock.Get<Organisation>(), Times.Once);
            _mockMapper.Verify(mock => mock.Map(It.IsAny<UpdateOrganisationRequest>(), It.IsAny<Organisation>()), Times.Once);
        }

        [Fact]
        public async Task UpdatingOrganisation_ThrowsExceptionWhenOrganisationNotFound()
        {
            // Arrange
            var nonExistentOrganisationRequest = new UpdateOrganisationRequest()
            {
                OrganisationId = Guid.NewGuid(),
                Address = new(),
                ContactDetails = new(),
            };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
                await _updateOrganisationInteractor.Handle(nonExistentOrganisationRequest, CancellationToken.None));
        }

        #endregion

    }
}
