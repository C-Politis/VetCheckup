using MediatR;
using Moq;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.Owners.DeleteOwner;
using VetCheckup.Domain.Entities;
using VetCheckup.Domain.Enums;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Owners.DeleteOwner;

public class DeleteOwnerInteractorTests
{

    #region Mithril

    private readonly Mock<IApplicationDbContext> _mockDbContext = new();
    private readonly DeleteOwnerRequest _request = new()
    {
        OwnerId = Guid.NewGuid()
    };
    private readonly IRequestHandler<DeleteOwnerRequest> _interactor;

    #endregion

    #region Constructor

    public DeleteOwnerInteractorTests()
    {
        this._mockDbContext
            .Setup(mock => mock.Get<Owner>())
            .Returns(new[] { new Owner()
                {
                    OwnerId = _request.OwnerId,
                    Address = new()
                    {
                        AddressId = Guid.NewGuid(),
                        Country = "Test Country",
                        PostalCode = "Test Postal Code",
                        State = "Test State",
                        StreetAddress = "Test Street Address",
                        Suburb = "Test Suburb",
                    },
                    ContactDetails = new()
                    {
                        ContactId = Guid.NewGuid(),
                        Email = string.Empty,
                        Mobile = string.Empty
                    },
                    User = new()
                    {
                        UserId =Guid.NewGuid(),
                        UserName = "MyUser",
                        Password = "Password",
                        UserType = UserType.OrganisationManager
                    },
                    FirstName = "Test",
                    LastName = "Owner",
                    MiddleName = "Middle",
                    Suffix = Suffix.Esq,
                    Title = Title.Dr,
                    Pets = new List<Pet>(),
                    DateOfBirth = DateTime.MinValue
                }
            }.AsQueryable());

        this._interactor = new DeleteOwnerInteractor(this._mockDbContext.Object);
    }

    #endregion

    #region Tests

    [Fact]
    public async Task DeleteOwner_WhenOwnerExists()
    {
        // Act
        await _interactor.Handle(_request, CancellationToken.None);

        // Assert
        this._mockDbContext.Verify(mock => mock.Remove(It.IsAny<Owner>()), Times.Once);
    }

    [Fact]
    public async Task DeleteOwner_ThrowsExceptionWhenOwnerNotFound()
    {
        // Arrange
        var invalidRequest = new DeleteOwnerRequest
        {
            OwnerId = Guid.NewGuid()
        };

        // Act
        await Assert.ThrowsAsync<Exception>(() => _interactor.Handle(invalidRequest, CancellationToken.None));

        // Assert
        this._mockDbContext.Verify(mock => mock.Remove(It.IsAny<Owner>()), Times.Never);
    }

    #endregion
}
