using AutoMapper;
using MediatR;
using Moq;
using VetCheckup.Application.Dtos;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.OrganisationManagers.GetOrganisationManager;
using VetCheckup.Domain.Enums;
using VetCheckup.Domain.Entities;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Organisations.GetOrganisationManager
{
    public class GetOrganisationManagerInteractorTests
    {
        #region Test Setup

        private readonly Mock<IDbContext> _mockDbContext = new();
        private readonly Mock<IMapper> _mockMapper = new();

        private readonly GetOrganisationManagerRequest _request;
        private readonly IRequestHandler<GetOrganisationManagerRequest, OrganisationManagerDto> _interactor;
        private readonly OrganisationManagerDto _organisationManagerDto = new()
        {
            OrganisationManagerId = Guid.NewGuid(),
            FirstName = "Gandalf",
            LastName = "The Grey",
            MiddleName = "Mithrandir",
            DateOfBirth = new DateTime(1990, 5, 15),
            Title = Title.Dr,
            Suffix = Suffix.None,
            Address = new AddressDto()
            {
                AddressId = Guid.NewGuid(),
                Country = "New Zealand",
                PostalCode = "3210",
                State = "Middle-earth",
                StreetAddress = "123 Wizard Lane",
                Suburb = "Hobbiton",
            },
            ContactDetails = new ContactDto()
            {
                ContactId = Guid.NewGuid(),
                Email = "gandalf@middleearth.com",
                Mobile = "+64 21 123 4567"
            },
            Organisation = new OrganisationDto()
            {
                OrganisationId = Guid.NewGuid(),
                Name = "Hobbiton Rescue",
                OrganisationType = OrganisationType.Other,
                Abn = string.Empty,
                Address = new Address()
                {
                    AddressId = Guid.NewGuid(),
                    Country = "Test Country",
                    PostalCode = "Test Postal Code",
                    State = "Test State",
                    StreetAddress = "Test Street Address",
                    Suburb = "Test Suburb",
                },
                ContactDetails = new Contact()
                {
                    ContactId = Guid.NewGuid(),
                    Email = string.Empty,
                    Mobile = string.Empty
                },
                VetOrganisations = new List<VetOrganisation>()
            }
        };

        #endregion

        #region Constructors

        public GetOrganisationManagerInteractorTests()
        {
            this._request = new()
            {
                OrganisationManagerId = _organisationManagerDto.OrganisationManagerId
            };

            // Create OrganisationManager
            var organisationManagerEntity = new OrganisationManager()
            {
                OrganisationManagerId = _organisationManagerDto.OrganisationManagerId,
                FirstName = "Gandalf",
                LastName = "The Grey",
                MiddleName = "Mithrandir",
                DateOfBirth = new DateTime(1990, 5, 15),
                Title = Title.Dr,
                Suffix = Suffix.None,
                Address = new()
                {
                    AddressId = Guid.NewGuid(),
                    Country = "New Zealand",
                    PostalCode = "3210",
                    State = "Middle-earth",
                    StreetAddress = "123 Wizard Lane",
                    Suburb = "Hobbiton",
                },
                ContactDetails = new()
                {
                    ContactId = Guid.NewGuid(),
                    Email = "gandalf@middleearth.com",
                    Mobile = "+64 21 123 4567"
                },
                Organisation = null
            };

            // Create Organisation
            var organisationEntity = new Organisation()
            {
                OrganisationId = _organisationManagerDto.Organisation?.OrganisationId ?? Guid.NewGuid(),
                Name = "Hobbiton Rescue",
                OrganisationType = OrganisationType.Other,
                Abn = string.Empty,
                VetOrganisations = new List<VetOrganisation>(),
                Address = new()
                {
                    AddressId = Guid.NewGuid(),
                    Country = string.Empty,
                    PostalCode = string.Empty,
                    State = string.Empty,
                    StreetAddress = string.Empty,
                    Suburb = string.Empty
                },
                ContactDetails = new()
                {
                    ContactId = Guid.NewGuid(),
                    Email = string.Empty,
                    Mobile = string.Empty
                },
                OrganisationManager = organisationManagerEntity
            };

            organisationManagerEntity.Organisation = organisationEntity;

            this._mockDbContext
                .Setup(mock => mock.Get<OrganisationManager>())
                .Returns(new[] { organisationManagerEntity }.AsQueryable());

            this._mockMapper
                .Setup(mock => mock.Map<OrganisationManagerDto>(It.IsAny<OrganisationManager>()))
                .Returns(this._organisationManagerDto);

            this._interactor = new GetOrganisationManagerInteractor(this._mockDbContext.Object, this._mockMapper.Object);
        }

        #endregion

        #region Handle Tests

        [Fact]
        public async Task GettingOrganisationManager_OrganisationManagerExists()
        {
            // Arrange
            var expectedOrganisationManager = this._organisationManagerDto;

            // Act
            var organisationManager = await this._interactor.Handle(this._request, default);

            // Assert
            Assert.Equal(expectedOrganisationManager, organisationManager);
        }

        [Fact]
        public async Task GettingOrganisationManager_ThrowsExceptionWhenOrganisationManagerNotFound()
        {
            // Arrange
            this._request.OrganisationManagerId = Guid.NewGuid();

            // Act
            var handleMethod = async () => await this._interactor.Handle(this._request, default);

            // Assert
            var exception = await Assert.ThrowsAsync<Exception>(handleMethod);
            Assert.Equal("Organisation Manager not found.", exception.Message);
        }

        [Fact]
        public async Task GettingOrganisationManager_ReturnsCorrectDetails()
        {
            // Arrange
            var expectedName = "Gandalf";
            var expectedLastName = "The Grey";
            var expectedTitle = Title.Dr;

            // Act
            var organisationManager = await this._interactor.Handle(this._request, default);

            // Assert
            Assert.Equal(expectedName, organisationManager.FirstName);
            Assert.Equal(expectedLastName, organisationManager.LastName);
            Assert.Equal(expectedTitle, organisationManager.Title);
            Assert.NotNull(organisationManager.ContactDetails);
            Assert.NotNull(organisationManager.Address);
            Assert.NotNull(organisationManager.Organisation);
        }

        [Fact]
        public async Task GettingOrganisationManager_MapsCorrectlyFromEntity()
        {
            // Arrange & Act
            await this._interactor.Handle(this._request, default);

            // Assert
            this._mockMapper.Verify(
                mock => mock.Map<OrganisationManagerDto>(It.IsAny<OrganisationManager>()),
                Times.Once);
        }

        [Fact]
        public async Task GettingOrganisationManager_QueriesDbContextCorrectly()
        {
            // Arrange & Act
            await this._interactor.Handle(this._request, default);

            // Assert
            this._mockDbContext.Verify(
                mock => mock.Get<OrganisationManager>(),
                Times.Once);
        }

        #endregion
    }
}
