using AutoMapper;
using MediatR;
using Moq;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.OrganisationManagers.UpdateOrganisationManager;
using VetCheckup.Domain.Entities;
using VetCheckup.Domain.Enums;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.OrganisationManagers.UpdateOrganisationManager;

public class UpdateOrganisationManagerInteractorTests
{
    #region Fields

    private readonly Mock<IApplicationDbContext> _mockContext = new();
    private readonly Mock<IMapper> _mockMapper = new();

    private readonly IRequestHandler<UpdateOrganisationManagerRequest> _updateOrganisationManagerInteractor;
    private readonly UpdateOrganisationManagerRequest _updateOrganisationRequest;

    #endregion


    #region Constructors

    public UpdateOrganisationManagerInteractorTests()
    {
        _updateOrganisationRequest = new UpdateOrganisationManagerRequest()
        {
            OrganisationManagerId = Guid.NewGuid(),
            Address = new()
            {
                StreetAddress = string.Empty,
                Country = string.Empty,
                PostalCode = string.Empty,
                State = string.Empty,
                Suburb = string.Empty,
            },
            ContactDetails = new() { Email = string.Empty, Mobile = string.Empty },
            FirstName = "Test",
            LastName = "Owner",
            MiddleName = "Middle",
            Suffix = Suffix.Esq,
            Title = Title.Dr,
            DateOfBirth = DateTime.MinValue
        };
        
        _mockContext
            .Setup(e => e.Get<Domain.Entities.OrganisationManager>())
            .Returns(new List<Domain.Entities.OrganisationManager> { new Domain.Entities.OrganisationManager
                    {
                        OrganisationManagerId = _updateOrganisationRequest.OrganisationManagerId,
                        Address = new Domain.Entities.Address()
                        {
                            AddressId = Guid.NewGuid(),
                            Country = "Country",
                            PostalCode = "PostalCode",
                            State = "State",
                            StreetAddress = "StreetAddress",
                            Suburb = "Suburb"
                        },
                         ContactDetails = new Domain.Entities.Contact()
                         {
                            ContactId = Guid.NewGuid(),
                            Email = string.Empty,
                            Mobile = string.Empty
                         },
                         User = new Domain.Entities.User()
                         {
                             UserId = Guid.NewGuid(),
                             UserName = "OldUsername",
                             Password = "OldPassword",
                             UserType = UserType.OrganisationManager
                         },
                         FirstName = "First",
                         LastName = "Last",
                         MiddleName = "Middle",
                         Suffix = Suffix.Esq,
                         Title = Title.Dr,
                         DateOfBirth = DateTime.UtcNow.AddYears(-20),
                         Organisation = null
                    } }.AsQueryable());

        _mockMapper
            .Setup(e => e.Map(It.IsAny<UpdateOrganisationManagerRequest>(), It.IsAny<OrganisationManager>()))
            .Callback((UpdateOrganisationManagerRequest request, OrganisationManager manager) => { });
        
        _updateOrganisationManagerInteractor = new UpdateOrganisationManagerInteractor(_mockContext.Object, _mockMapper.Object);
    }

    #endregion


    #region Tests

    [Fact]
    public async Task Handle_ValidRequest_UpdatesOrganisationManager()
    {
        // Arrange
        var request = _updateOrganisationRequest;

        // Act
        await _updateOrganisationManagerInteractor.Handle(request, CancellationToken.None);

        // Assert
        _mockMapper.Verify(e => e.Map(request, It.IsAny<OrganisationManager>()), Times.Once);
    }
    
    [Fact]
    public async Task Handle_NonExistentOrganisationManager_ThrowsException()
    {
        // Arrange
        var request = new UpdateOrganisationManagerRequest
        {
            OrganisationManagerId = Guid.NewGuid(), // Non-existent ID
            Address = new()
            {
                StreetAddress = string.Empty,
                Country = string.Empty,
                PostalCode = string.Empty,
                State = string.Empty,
                Suburb = string.Empty,
            },
            ContactDetails = new() { Email = string.Empty, Mobile = string.Empty },
            FirstName = "Test",
            LastName = "Owner",
            MiddleName = "Middle",
            Suffix = Suffix.Esq,
            Title = Title.Dr,
            DateOfBirth = DateTime.MinValue
        };

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _updateOrganisationManagerInteractor.Handle(request, CancellationToken.None));
        _mockMapper.Verify(e => e.Map(It.IsAny<UpdateOrganisationManagerRequest>(), It.IsAny<OrganisationManager>()), Times.Never);
    }

    #endregion
}
