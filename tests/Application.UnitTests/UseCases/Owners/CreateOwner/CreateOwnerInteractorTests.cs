using AutoMapper;
using MediatR;
using Moq;
using NUnit.Framework;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.Owners.CreateOwner;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UnitTests.UseCases.Owners.CreateOwner;

public class CreateOwnerInteractorTests
{

    #region Fields

    private readonly Mock<IDbContext> _mockContext = new();
    private readonly Mock<IMapper> _mockMapper = new();

    private readonly IRequestHandler<CreateOwnerRequest> _createOwnerInteractor;
    private readonly CreateOwnerRequest _createOwnerRequest;

    #endregion

    #region Constructors

    public CreateOwnerInteractorTests()
    {
        _createOwnerRequest = new CreateOwnerRequest()
        {
            Address = new(),
            ContactDetails = new(),
            Name = "New Owner",
            DateOfBirth = new DateTime(2000, 01, 01)
        };

        _mockMapper
            .Setup(e => e.Map<Owner>(It.IsAny<CreateOwnerRequest>()))
            .Returns(() => new Owner()
            {
                Address = new() { Country = string.Empty, PostalCode = string.Empty, State = string.Empty, StreetAddress = string.Empty, Suburb = string.Empty },
                ContactDetails = new() { Email = string.Empty, Mobile = 1 },
                Name = _createOwnerRequest.Name,
                DateOfBirth = _createOwnerRequest.DateOfBirth
            });


        _createOwnerInteractor = new CreateOwnerInteractor(this._mockContext.Object, this._mockMapper.Object);
    }

    #endregion

    #region Interactor Tests

    [Test]
    public async Task CreatingOwner_AddsNewOwnerToContext()
    {

        await this._createOwnerInteractor.Handle(_createOwnerRequest, CancellationToken.None);

        this._mockContext.Verify(mock => mock.Add(It.IsAny<Owner>()), Times.Once);
    }

    #endregion

}
