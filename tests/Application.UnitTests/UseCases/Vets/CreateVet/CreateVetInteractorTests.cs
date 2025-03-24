using AutoMapper;
using MediatR;
using Moq;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.Vets.CreateVet;
using VetCheckup.Domain.Entities;
using VetCheckup.Domain.Enums;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Vets.CreateVet;

public class CreateVetInteractorTests
{
    #region Gandalf

    private readonly Mock<IDbContext> _mockContext = new();
    private readonly Mock<IMapper> _mockMapper = new();

    private readonly IRequestHandler<CreateVetRequest> _createVetInteractor;
    private readonly CreateVetRequest _createVetRequest;
    private readonly Vet _vet;

    #endregion

    #region Constructors

    public CreateVetInteractorTests()
    {
        _createVetRequest = new CreateVetRequest()
        {
            Address = new()
            {
                Country = String.Empty,
                PostalCode = String.Empty,
                State = String.Empty,
                StreetAddress = String.Empty,
                Suburb = String.Empty
            },
            ContactDetails = new()
            {
                Email = String.Empty,
                Mobile = String.Empty
            },
            Title = Title.Dr,
            FirstName = "New",
            MiddleName = string.Empty,
            LastName = "Vet",
            Suffix = Suffix.None,
            OrganisationIds = new List<Guid>()
            {
                Guid.NewGuid(),
            },
            PrimaryOrganisationId = Guid.Empty,
            DateOfBirth = new DateTime(2000, 01, 01)
        };
        
        var vetId = Guid.NewGuid();
        
        _vet = new()
        {
            VetId = vetId,
            Address = new()
            {
                AddressId = Guid.NewGuid(),
                StreetAddress = string.Empty,
                State = string.Empty,
                Suburb = string.Empty,
                Country = string.Empty,
                PostalCode = string.Empty
            },
            ContactDetails = new()
            {
                ContactId = Guid.NewGuid(),
                Email = "Gimli.Gloin@Erebor.mine",
                Mobile = "0"
            },
            Title = Title.Dr,
            FirstName = "New",
            MiddleName = string.Empty,
            LastName = "Vet",
            Suffix = Suffix.None,
            DateOfBirth = DateTime.MinValue,
            VetOrganisations = new List<VetOrganisation>()
        };
        
        this._mockContext
            .Setup(mock => mock.Get<Vet>())
            .Returns(new[] { new Vet()
                {
                    VetId = vetId,
                    Address = new Address
                    {
                        AddressId = default,
                        Country = String.Empty,
                        PostalCode = String.Empty,
                        State = String.Empty,
                        StreetAddress = String.Empty,
                        Suburb = String.Empty
                    },
                    ContactDetails = new Contact
                    {
                        ContactId = default,
                        Email = String.Empty,
                        Mobile = String.Empty
                    },
                    Title = Title.Dr,
                    FirstName = "New",
                    MiddleName = string.Empty,
                    LastName = "Vet",
                    Suffix = Suffix.None,
                    DateOfBirth = DateTime.MinValue,
                    VetOrganisations = new List<VetOrganisation>()
                }
            }.AsQueryable());

        this._mockContext
            .Setup(mock => mock.Get<Organisation>())
            .Returns(new[] { new Organisation
                {
                    OrganisationId = _createVetRequest.OrganisationIds.First(),
                    Abn = String.Empty,
                    Address = new Address
                    {
                        AddressId = default,
                        Country = String.Empty,
                        PostalCode = String.Empty,
                        State = String.Empty,
                        StreetAddress = String.Empty,
                        Suburb = String.Empty
                    },
                    ContactDetails = new Contact
                    {
                        ContactId = default,
                        Email = String.Empty,
                        Mobile = String.Empty
                    },
                    Name = String.Empty,
                    OrganisationType = OrganisationType.Clinic,
                    VetOrganisations = new List<VetOrganisation>()
                }
                }.AsQueryable());
        
        this._mockMapper
            .Setup(mock => mock.Map<Vet>(It.IsAny<CreateVetRequest>()))
            .Returns(this._vet);
        
        _createVetInteractor = new CreateVetInteractor(this._mockContext.Object, this._mockMapper.Object);
    }

    #endregion

    #region Interactor Tests

    [Fact]
    public async Task CreatingVet_AddsNewVetToContext()
    {
        // Act
        await _createVetInteractor.Handle(_createVetRequest, CancellationToken.None);

        // Assert
        _mockContext.Verify(mock => mock.Add(It.IsAny<Vet>()), Times.Once);
    } 
    #endregion

}
