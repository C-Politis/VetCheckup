using MediatR;
using Moq;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.Pets.DeletePet;
using VetCheckup.Domain.Entities;
using VetCheckup.Domain.Enums;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Pets.DeletePet;

public class DeletePetInteractorTests
{
    #region Secret Fire

    private readonly Mock<IDbContext> _mockDbContext = new();
    
    private readonly DeletePetRequest _request = new()
    {
        PetId = Guid.NewGuid()
    };
    private readonly IRequestHandler<DeletePetRequest> _interactor;
    
    #endregion
    
    #region Constructor
    
    public DeletePetInteractorTests()
    {
        this._mockDbContext
            .Setup(mock => mock.Get<Pet>())
            .Returns(new[] { new Pet()
            {
                PetId = this._request.PetId,
                DateOfBirth = DateTime.Now,
                MicrochipId = "123456",
                Name = "Test Pet",
                Sex = Sex.Male,
                Species = "Test Species",
                Owner = new()
                {
                    OwnerId = Guid.NewGuid(),
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
                    FirstName = "Test",
                    LastName = "Owner",
                    MiddleName = "Middle",
                    Suffix = Suffix.Esq,
                    Title = Title.Dr,
                    Pets = new List<Pet>(),
                    DateOfBirth = DateTime.MinValue
                }
            } }.AsQueryable());
        
        _interactor = new DeletePetInteractor(this._mockDbContext.Object);
    }
    
    #endregion
    
    
    #region Tests

    [Fact]
    public async Task DeletePet_WhenPetExists()
    {
        // Act
        await _interactor.Handle(_request, CancellationToken.None);
        
        // Assert
        this._mockDbContext.Verify(mock => mock.Remove(It.IsAny<Pet>()), Times.Once);
    }


    [Fact]
    public async Task DeletePet_ThrowsExceptionWhenPetNotFound()
    {
        // Arrange
        var invalidRequest = new DeletePetRequest
        {
            PetId = Guid.NewGuid()
        };

        //Act
        await Assert.ThrowsAsync<Exception>(() => _interactor.Handle(invalidRequest, CancellationToken.None));

        //Asset
        this._mockDbContext.Verify(mock => mock.Remove(It.IsAny<Pet>()), Times.Never);
    }

    #endregion

}
