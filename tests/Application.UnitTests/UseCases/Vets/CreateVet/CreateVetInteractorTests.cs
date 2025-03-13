using AutoMapper;
using MediatR;
using Moq;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.Vets.CreateVet;
using VetCheckup.Domain.Entities;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Vets.CreateVet;

public class CreateVetInteractorTests
{
    #region Gandalf

    private readonly Mock<IDbContext> _mockContext = new();
    private readonly Mock<IMapper> _mockMapper = new();

    private readonly IRequestHandler<CreateVetRequest> _createVetInteractor;
    private readonly CreateVetRequest _createVetRequest;

    #endregion

    #region Constructors

    public CreateVetInteractorTests()
    {
        _createVetRequest = new CreateVetRequest()
        {
            Address = new(),
            ContactDetails = new(),
            Name = "New Vet",
            DateOfBirth = new DateTime(2000, 01, 01)
        };

        _createVetInteractor = new CreateVetInteractor(this._mockContext.Object, this._mockMapper.Object);
    }

    #endregion

    #region Interactor Tests

    [Fact]
    public async Task CreatingVet_AddsNewVetToContext()
    {
        // Act
        await this._createVetInteractor.Handle(_createVetRequest, CancellationToken.None);

        // Assert
        this._mockContext.Verify(mock => mock.Add(It.IsAny<Vet>()), Times.Once);
    } 
    #endregion

}
