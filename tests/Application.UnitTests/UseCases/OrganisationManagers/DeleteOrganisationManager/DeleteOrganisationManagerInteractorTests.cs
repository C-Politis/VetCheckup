using MediatR;
using Moq;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.OrganisationManagers.DeleteOrganisationManager;
using VetCheckup.Domain.Entities;
using VetCheckup.Domain.Enums;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.OrganisationManagers.DeleteOrganisationManager;

public class DeleteOrganisationManagerInteractorTests
{

    #region Fields

    private readonly Mock<IDbContext> _mockContext = new();

    private readonly IRequestHandler<DeleteOrganisationManagerRequest> _deleteOrganisationManagerInteractor;
    private readonly DeleteOrganisationManagerRequest _deleteOrganisationManagerRequest;

    #endregion

    #region Constructors

    public DeleteOrganisationManagerInteractorTests()
    {
        _deleteOrganisationManagerRequest = new DeleteOrganisationManagerRequest()
        {
            OrganisationManagerId = Guid.Empty
        };

        _mockContext
            .Setup(e => e.Get<OrganisationManager>())
            .Returns(new List<OrganisationManager> { new OrganisationManager
                {
                    FirstName = "Bob",
                    LastName = "Bobson",
                    DateOfBirth = DateTime.MaxValue,
                    OrganisationManagerId = _deleteOrganisationManagerRequest.OrganisationManagerId,
                    Address = new Address()
                    {
                    AddressId = Guid.NewGuid(),
                    Country = string.Empty,
                    PostalCode = string.Empty,
                    State = string.Empty,
                    StreetAddress = string.Empty,
                    Suburb = string.Empty
                    },
                    MiddleName = string.Empty,
                    Suffix = Suffix.None,
                    ContactDetails = new Contact()
                    {
                        Email = "",
                        Mobile = "",
                        ContactId = Guid.NewGuid(),
                    },
                    Title = Title.Dr
                }
            }.AsQueryable());

        _deleteOrganisationManagerInteractor = new DeleteOrganisationManagerInteractor(this._mockContext.Object);
    }

    #endregion

    #region Handle tests

    [Fact]
    public async Task DeleteOrganisationManager_OrganisationManagerExists()
    {
        // Arrange
        await this._deleteOrganisationManagerInteractor.Handle(_deleteOrganisationManagerRequest, CancellationToken.None);

        // Assert
        this._mockContext.Verify(mock => mock.Remove(It.IsAny<OrganisationManager>()), Times.Once);
    }

    [Fact]
    public async Task DeleteOrganisationManager_OrganisationManagerNotFound()
    {
        // Arrange
        var nonExistentOrganisationManagerRequest = new DeleteOrganisationManagerRequest()
        {
            OrganisationManagerId = Guid.NewGuid()
        };

        // Assert
        await Assert.ThrowsAsync<Exception>(async () =>
             await this._deleteOrganisationManagerInteractor.Handle(nonExistentOrganisationManagerRequest, CancellationToken.None));
    }

    #endregion
}
