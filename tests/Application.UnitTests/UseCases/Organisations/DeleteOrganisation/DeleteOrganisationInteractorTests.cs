using System.Xml.Linq;
using MediatR;
using Moq;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.Organisations.CreateOrganisation;
using VetCheckup.Application.UseCases.Organisations.DeleteOrganisation;
using VetCheckup.Domain.Entities;
using VetCheckup.Domain.Enums;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Organisations.DeleteOrganisation;

public class DeleteOrganisationInteractorTests
{

    #region Treebeard

    private readonly Mock<IDbContext> _mockDbContext = new();
    private readonly DeleteOrganisationRequest _request = new()
    {
        OrganisationId = Guid.NewGuid()
    };
    private readonly IRequestHandler<DeleteOrganisationRequest> _interactor;

    #endregion

    #region Constructor

    public DeleteOrganisationInteractorTests()
    {
        this._mockDbContext
            .Setup(mock => mock.Get<Organisation>())
            .Returns(new[] { new Organisation()
            {
                OrganisationId = _request.OrganisationId,
                Abn = "51824753556",
                Address = new()
                {
                    AddressId = Guid.NewGuid(),
                    StreetAddress = string.Empty,
                    Country = string.Empty,
                    PostalCode = string.Empty,
                    State = string.Empty,
                    Suburb = string.Empty,
                },
                ContactDetails = new()
                {
                    ContactId = Guid.NewGuid(),
                    Email = string.Empty,
                    Mobile = string.Empty
                },
                Name = "Giant Eagles Rescue",
                OrganisationType = Domain.Enums.OrganisationType.Rescues,
                VetOrganisations = new List<VetOrganisation>(),
                OrganisationManager = new OrganisationManager()
                {
                    FirstName = "Bob",
                    LastName = "Bobson",
                    DateOfBirth = DateTime.MaxValue,
                    OrganisationManagerId = Guid.NewGuid(),
                    Address = new Address()
                    {
                        AddressId = Guid.NewGuid(),
                        Country = string.Empty,
                        PostalCode = string.Empty,
                        State = string.Empty,
                        StreetAddress = string.Empty,
                        Suburb = string.Empty
                    },
                    ContactDetails = new Contact()
                    {
                        ContactId = Guid.NewGuid(),
                        Email = string.Empty,
                        Mobile = string.Empty
                    },
                    User = new User()
                    {
                        UserId = Guid.NewGuid(),
                        UserName = string.Empty,
                        UserType = UserType.OrganisationManager,
                        Password = "Password"
                    },
                    Title = Title.Dr,
                    MiddleName = "A.",
                    Suffix = Suffix.II,
                    Organisation = null
                }
            } }.AsQueryable());

        this._interactor = new DeleteOrganisationInteractor(this._mockDbContext.Object);
    }

    #endregion

    #region Tests

    [Fact]
    public async Task DeleteOrganisation_WhenOrganisationExists()
    {
        // Act
        await _interactor.Handle(_request, CancellationToken.None);

        // Assert
        this._mockDbContext.Verify(mock => mock.Remove(It.IsAny<Organisation>()), Times.Once);
    }

    [Fact]
    public async Task DeleteOrganisation_ThrowsExceptionWhenOrganisationNotFound()
    {
        // Arrange
        var invalidRequest = new DeleteOrganisationRequest
        {
            OrganisationId = Guid.NewGuid()
        };

        // Act
        await Assert.ThrowsAsync<Exception>(() => _interactor.Handle(invalidRequest, CancellationToken.None));

        // Assert
        this._mockDbContext.Verify(mock => mock.Remove(It.IsAny<Organisation>()), Times.Never);
    }

    #endregion

}
