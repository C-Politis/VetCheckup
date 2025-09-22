using AutoMapper;
using MediatR;
using Moq;
using VetCheckup.Application.Dtos;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.OrganisationManagers.GetOrganisationManager;
using VetCheckup.Domain.Enums;
using VetCheckup.Domain.Entities;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.OrganisationManagers.GetOrganisation
{
    public class GetOrganisationManagerInteractorTests
    {
        #region Tests Shall Not Pass

        private readonly Mock<IApplicationDbContext> _mockDbContext = new();
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
            _request = new()
            {
                OrganisationManagerId = _organisationManagerDto.OrganisationManagerId
            };

            // Create OrganisationManager
            var organisationManagerEntity = new OrganisationManager()
            {
                User = new User()
                {
                    Password = string.Empty,
                    UserId = Guid.NewGuid(),
                    UserName = string.Empty,
                    UserType = UserType.OrganisationManager
                },
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

            _mockDbContext
                .Setup(mock => mock.Get<OrganisationManager>())
                .Returns(new[] { organisationManagerEntity }.AsQueryable());

            _mockMapper
                .Setup(mock => mock.Map<OrganisationManagerDto>(It.IsAny<OrganisationManager>()))
                .Returns(_organisationManagerDto);

            _interactor = new GetOrganisationManagerInteractor(_mockDbContext.Object, _mockMapper.Object);
        }

        #endregion

        #region Handle Tests

        [Fact]
        public async Task GettingOrganisationManager_OrganisationManagerExists()
        {
            // Arrange
            var expectedOrganisationManager = _organisationManagerDto;

            // Act
            var organisationManager = await _interactor.Handle(_request, default);

            // Assert
            Assert.Equal(expectedOrganisationManager, organisationManager);
        }

        [Fact]
        public async Task GettingOrganisationManager_ThrowsExceptionWhenOrganisationManagerNotFound()
        {
            // Arrange
            _request.OrganisationManagerId = Guid.NewGuid();

            // Act
            var handleMethod = async () => await _interactor.Handle(_request, default);

            // Assert
            var exception = await Assert.ThrowsAsync<Exception>(handleMethod);
            Assert.Equal("Organisation Manager not found.", exception.Message);
        }

        #endregion
    }
}
