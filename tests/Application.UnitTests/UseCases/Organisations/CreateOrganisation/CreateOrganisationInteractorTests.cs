using AutoMapper;
using MediatR;
using Moq;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.Organisations.CreateOrganisation;
using VetCheckup.Domain.Entities;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Organisations.CreateOrganisation;

public class CreateOrganisationInteractorTests
{

    #region Fields

    private readonly Mock<IDbContext> _mockContext = new();
    private readonly Mock<IMapper> _mockMapper = new();

    private readonly IRequestHandler<CreateOrganisationRequest> _createOrganisationInteractor;
    private readonly CreateOrganisationRequest _createOrganisationRequest;

    #endregion

    #region Constructors

    public CreateOrganisationInteractorTests()
    {
        _createOrganisationRequest = new CreateOrganisationRequest()
        {
            Abn = "02110000000",
            Address = new(),
            ContactDetails = new(),
            Name = "Giant Eagles Rescue",
            OrganisationType = Domain.Enums.OrganisationType.Rescues
        };

        _mockMapper
            .Setup(e => e.Map<Organisation>(It.IsAny<CreateOrganisationRequest>()))
            .Returns(() => new Organisation()
            {
                Abn = _createOrganisationRequest.Abn,
                Address = new() { Country = string.Empty, PostalCode = string.Empty, State = string.Empty, StreetAddress = string.Empty, Suburb = string.Empty },
                ContactDetails = new() { Email = string.Empty, Mobile = "1" },
                Name = _createOrganisationRequest.Name,
                OrganisationType = _createOrganisationRequest.OrganisationType
            });


        _createOrganisationInteractor = new CreateOrganisationInteractor(this._mockContext.Object, this._mockMapper.Object);
    }

    #endregion

    #region Interactor Tests

    [Fact]
    public async Task CreatingOrganisation_AddsNewOrganisationToContext()
    {
        //Act
        await this._createOrganisationInteractor.Handle(_createOrganisationRequest, CancellationToken.None);

        //Assert
        this._mockContext.Verify(mock => mock.Add(It.IsAny<Organisation>()), Times.Once);
    }

    #endregion
}
