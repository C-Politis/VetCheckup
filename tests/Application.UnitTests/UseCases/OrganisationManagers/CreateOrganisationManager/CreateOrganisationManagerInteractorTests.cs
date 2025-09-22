using AutoMapper;
using MediatR;
using Moq;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.OrganisationManagers.CreateOrganisationManager;
using VetCheckup.Domain.Entities;
using VetCheckup.Domain.Enums;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.OrganisationManagers.CreateOrganisationManager;

public class CreateOrganisationManagerInteractorTests
{

    #region Fields

    private readonly Mock<IApplicationDbContext> _mockContext = new();
    private readonly Mock<IMapper> _mockMapper = new();

    private readonly IRequestHandler<CreateOrganisationManagerRequest> _createOrganisationManagerInteractor;
    private readonly CreateOrganisationManagerRequest _createOrganisationManagerRequest;

    #endregion

    #region Constructors

    public CreateOrganisationManagerInteractorTests()
    {
        _createOrganisationManagerRequest = new CreateOrganisationManagerRequest()
        {
            User = new()
            {
                Password = string.Empty,
                UserName = string.Empty,
                UserType = UserType.OrganisationManager
            },
            Address = new()
            {
                StreetAddress = string.Empty,
                Country = string.Empty,
                PostalCode = string.Empty,
                State = string.Empty,
                Suburb = string.Empty,
            },
            ContactDetails = new()
            {
                Email = string.Empty,
                Mobile = string.Empty
            },
            FirstName = "Test",
            LastName = "Owner",
            MiddleName = "Middle",
            Suffix = Suffix.Esq,
            Title = Title.Dr,
            DateOfBirth = new DateTime(2000, 01, 01),
        };

        _mockMapper
            .Setup(e => e.Map<OrganisationManager>(It.IsAny<CreateOrganisationManagerRequest>()))
            .Returns(() => new OrganisationManager
            {
                OrganisationManagerId = Guid.NewGuid(),
                User = new()
                {
                    UserId = Guid.NewGuid(),
                    Password = string.Empty,
                    UserName = string.Empty,
                    UserType = UserType.OrganisationManager
                },
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
                FirstName = "Test",
                LastName = "Owner",
                MiddleName = "Middle",
                Suffix = Suffix.Esq,
                Title = Title.Dr,
                DateOfBirth = new DateTime(2000, 01, 01),
            });

        _createOrganisationManagerInteractor = new CreateOrganisationManagerInteractor(this._mockContext.Object, this._mockMapper.Object);
    }

    #endregion

    #region Interactor Tests

    [Fact]
    public async Task CreateOrganisationManager_AddsNewOrganisationManagerToContext()
    {
        //Act
        await this._createOrganisationManagerInteractor.Handle(_createOrganisationManagerRequest, CancellationToken.None);

        //Assert
        this._mockContext.Verify(mock => mock.Add(It.IsAny<OrganisationManager>()), Times.Once);
    }

    #endregion

}
