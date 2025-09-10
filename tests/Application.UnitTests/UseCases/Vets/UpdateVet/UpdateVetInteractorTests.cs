using AutoMapper;
using MediatR;
using Moq;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.Vets.UpdateVet;
using VetCheckup.Domain.Entities;
using VetCheckup.Domain.Enums;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Vets.UpdateVet
{
    public class UpdateVetInteractorTests
    {

        #region Fields

        private readonly Mock<IDbContext> _mockContext = new();
        private readonly Mock<IMapper> _mockMapper = new();

        private readonly IRequestHandler<UpdateVetRequest> _updateVetInteractor;
        private readonly UpdateVetRequest _updateVetRequest;

        #endregion

        #region Constructors

        public UpdateVetInteractorTests()
        {
            _updateVetRequest = new UpdateVetRequest()
            {
                VetId = Guid.NewGuid(),
                Address = new(),
                ContactDetails = new(),
            };

            _mockContext
                .Setup(e => e.Get<Vet>())
                .Returns(new List<Vet> { new Vet
                    {
                        VetId = _updateVetRequest.VetId,
                        Address = new()
                        {
                            AddressId = Guid.NewGuid(),
                            Country = "Country",
                            PostalCode = "PostalCode",
                            State = "State",
                            StreetAddress = "StreetAddress",
                            Suburb = "Suburb"
                        },
                        ContactDetails = new()
                        {
                            ContactId = Guid.NewGuid(),
                            Email = string.Empty,
                            Mobile = string.Empty
                        },
                        User = new User()
                        {
                            UserId = Guid.NewGuid(),
                            UserName = "MyUser",
                            Password = "Password",
                            UserType = UserType.Organisation
                        },
                        Title = Title.None,
                        FirstName = "Old First",
                        MiddleName = string.Empty,
                        LastName = "Old Last",
                        Suffix = Suffix.None,
                        DateOfBirth = DateTime.MinValue,
                        VetOrganisations = new List<VetOrganisation>()
                        {
                            new VetOrganisation
                            {
                                Organisation = new Organisation
                                {
                                    OrganisationId = Guid.NewGuid(),
                                    Name = "Organisation",
                                    Address = new()
                                    {
                                        AddressId = Guid.NewGuid(),
                                        Country = "Country",
                                        PostalCode = "PostalCode",
                                        State = "State",
                                        StreetAddress = "StreetAddress",
                                        Suburb = "Suburb"
                                    },
                                    ContactDetails = new()
                                    {
                                        ContactId = Guid.NewGuid(),
                                        Email = string.Empty,
                                        Mobile = string.Empty
                                    },
                                    Abn = "51824753556",
                                    OrganisationType = OrganisationType.Clinic,
                                    VetOrganisations = new List<VetOrganisation>(),
                                    OrganisationManager = new OrganisationManager()
                                    {
                                        OrganisationManagerId = Guid.NewGuid(),
                                        FirstName = "Bob",
                                        LastName = "Bobson",
                                        DateOfBirth = DateTime.MaxValue,
                                        Address = new()
                                        {
                                            AddressId = Guid.NewGuid(),
                                            Country = "Country",
                                            PostalCode = "PostalCode",
                                            State = "State",
                                            StreetAddress = "StreetAddress",
                                            Suburb = "Suburb"
                                        },
                                        ContactDetails = new()
                                        {
                                            ContactId = Guid.NewGuid(),
                                            Email = string.Empty,
                                            Mobile = string.Empty
                                        },
                                        User = new User()
                                        {
                                            UserId = Guid.NewGuid(),
                                            UserName = "MyUser",
                                            Password = "Password",
                                            UserType = UserType.Organisation
                                        },
                                        Title = Title.Dr,
                                        MiddleName = "A.",
                                        Suffix = Suffix.II,
                                        Organisation = null
                                    }
                                },
                                IsPrimaryOrganisation = true,
                                Vet = new Vet
                                {
                                    VetId = _updateVetRequest.VetId,
                                    Address = new()
                                    {
                                        AddressId = Guid.NewGuid(),
                                        Country = "Country",
                                        PostalCode = "PostalCode",
                                        State = "State",
                                        StreetAddress = "StreetAddress",
                                        Suburb = "Suburb"
                                    },
                                    ContactDetails = new()
                                    {
                                        ContactId = Guid.NewGuid(),
                                        Email = string.Empty,
                                        Mobile = string.Empty
                                    },
                                    User = new User()
                                    {
                                        UserId = Guid.NewGuid(),
                                        UserName = "MyUser",
                                        Password = "Password",
                                        UserType = UserType.Organisation
                                    },
                                    Title = Title.None,
                                    FirstName = "Old First",
                                    MiddleName = string.Empty,
                                    LastName = "Old Last",
                                    Suffix = Suffix.None,
                                    DateOfBirth = DateTime.MinValue,
                                    VetOrganisations = new List<VetOrganisation>()
                            }
                        } 
                        }
                    }
                }.AsQueryable());

            _mockMapper
                .Setup(e => e.Map(It.IsAny<UpdateVetRequest>(), It.IsAny<Vet>()))
                .Callback<UpdateVetRequest, Vet>((request, vet) =>
                {
                    vet.FirstName = request.FirstName ?? vet.FirstName;
                    vet.MiddleName = request.MiddleName ?? vet.MiddleName;
                    vet.LastName = request.LastName ?? vet.LastName;
                    vet.Title = request.Title ?? vet.Title;
                    vet.Suffix = request.Suffix ?? vet.Suffix;
                    vet.DateOfBirth = request.DateOfBirth ?? vet.DateOfBirth;
                });

            _updateVetInteractor = new UpdateVetInteractor(this._mockContext.Object, this._mockMapper.Object);
        }

        #endregion

        #region Interactor Tests

        [Fact]
        public async Task UpdatingVet_UpdatesExistingPetToContextAsync()
        {
            // Act
            await _updateVetInteractor.Handle(_updateVetRequest, CancellationToken.None);

            // Assert
            _mockContext.Verify(mock => mock.Get<Vet>(), Times.Once);
            _mockMapper.Verify(mock => mock.Map(It.IsAny<UpdateVetRequest>(), It.IsAny<Vet>()), Times.Once);
        }

        [Fact]
        public async Task UpdatingVet_ThrowsExceptionWhenVetNotFound()
        {
            // Arrange
            var nonExistentVetRequest = new UpdateVetRequest
            {
                VetId = Guid.NewGuid(),
                Address = new(),
                ContactDetails = new(),
                DateOfBirth = new DateTime(2010, 01, 01)
            };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
                await _updateVetInteractor.Handle(nonExistentVetRequest, CancellationToken.None));
        }
        
        [Fact]
        public async Task UpdatingVet_ThrowsExceptionWhenOrganisationIdsDoNotExist()
        {
            // Arrange
            var nonExistentOrganisationId = Guid.NewGuid();
            var nonExistentOrganisationRequest = new UpdateVetRequest
            {
                VetId = _updateVetRequest.VetId,
                Address = new(),
                ContactDetails = new(),
                DateOfBirth = new DateTime(2010, 01, 01),
                OrganisationIds = new List<Guid> { nonExistentOrganisationId }
            };

            _mockContext
                .Setup(e => e.Get<Organisation>())
                .Returns(new List<Organisation>().AsQueryable());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
                await _updateVetInteractor.Handle(nonExistentOrganisationRequest, CancellationToken.None));
        }

        #endregion

    }
}
