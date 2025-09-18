using MediatR;
using Moq;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.Vets.DeleteVet;
using VetCheckup.Domain.Entities;
using VetCheckup.Domain.Enums;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Vets.DeleteVet;

public class DeleteVetInteractorTests
{
    #region Deeds will not be less valiant because they are unpraised

    private readonly Mock<IDbContext> _mockDbContext = new();
    
    private readonly DeleteVetRequest _request = new()
    {
        VetId = Guid.NewGuid()
    };
    private readonly IRequestHandler<DeleteVetRequest> _interactor;

    #endregion
    
    
    #region Constructors
    
    public DeleteVetInteractorTests()
    {
        this._mockDbContext
            .Setup(e => e.Get<Domain.Entities.Vet>())
            .Returns(new List<Domain.Entities.Vet>
            {
                new()
                {
                    VetId = _request.VetId,
                    FirstName = "John",
                    LastName = "Doe",
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
                    DateOfBirth = DateTime.Now,
                    User = new User()
                    {
                        UserId = default,
                        UserName = "MyUser",
                        Password = "Password",
                        UserType = UserType.OrganisationManager
                    },
                    Title = Title.None,
                    MiddleName = "null",
                    Suffix = Suffix.None,
                    VetOrganisations = new List<VetOrganisation>(),
                }
            }.AsQueryable());
                        
        _interactor = new DeleteVetInteractor(_mockDbContext.Object);
    }
    
    #endregion

    #region Tests

    [Fact]
    public async Task Handle_Should_Delete_Vet()
    {
        // Act
        await _interactor.Handle(_request, CancellationToken.None);
        
        // Assert
        _mockDbContext.Verify(e => e.Remove(It.Is<Domain.Entities.Vet>(v => v.VetId == _request.VetId)), Times.Once);
    }
    
    [Fact]
    public async Task Handle_Should_Throw_Exception_When_Vet_Not_Found()
    {
        // Arrange
        var invalidRequest = new DeleteVetRequest
        {
            VetId = Guid.NewGuid()
        };
        
        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _interactor.Handle(invalidRequest, CancellationToken.None));
    }

    #endregion
}
