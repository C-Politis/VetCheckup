using AutoMapper;
using MediatR;
using Moq;
using VetCheckup.Application.Common.Enums;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.Owners.CreateOwner;
using VetCheckup.Domain.Entities;
using Xunit;

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
            DateOfBirth = new DateTime(2000, 01, 01)
        };

        _mockMapper
            .Setup(e => e.Map<Owner>(It.IsAny<CreateOwnerRequest>()))
            .Returns(() => new Owner()
            {
                OwnerId = Guid.NewGuid(),
                Title = Title.None,
                Address = new() { AddressId = Guid.NewGuid(), Country = string.Empty, PostalCode = string.Empty, State = string.Empty, StreetAddress = string.Empty, Suburb = string.Empty },
                ContactDetails = new() { ContactId = Guid.NewGuid(), Email = string.Empty, Mobile = "1" },
                FirstName = _createOwnerRequest.FirstName,
                LastName = _createOwnerRequest.LastName,
                MiddleName = _createOwnerRequest.MiddleName,
                Suffix = _createOwnerRequest.Suffix,
                DateOfBirth = _createOwnerRequest.DateOfBirth,
                Pets = new List<Pet>()
            });


        _createOwnerInteractor = new CreateOwnerInteractor(this._mockContext.Object, this._mockMapper.Object);
    }

    #endregion

    #region Interactor Tests

    [Fact]
    public async Task CreatingOwner_AddsNewOwnerToContext()
    {
        //Act
        await this._createOwnerInteractor.Handle(_createOwnerRequest, CancellationToken.None);

        //Assert
        this._mockContext.Verify(mock => mock.Add(It.IsAny<Owner>()), Times.Once);
    }

    #endregion

}
