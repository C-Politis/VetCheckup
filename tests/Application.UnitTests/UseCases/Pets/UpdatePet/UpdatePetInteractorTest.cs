
using System.Threading;
using AutoMapper;
using MediatR;
using Moq;
using NUnit.Framework;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.Pets.UpdatePet;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UnitTests.UseCases.Pets.UpdatePet
{
    public class UpdatePetInteractorTest
    {
        #region Fields

        private readonly Mock<IDbContext> _mockContext = new();
        private readonly Mock<IMapper> _mockMapper = new();

        private readonly IRequestHandler<UpdatePetRequest> _updatePetInteractor;
        private readonly UpdatePetRequest _updatePetRequest;

        #endregion

        #region Constructor

        public UpdatePetInteractorTest()
        {
            _updatePetRequest = new UpdatePetRequest
            {
                PetId = Guid.NewGuid(),
                Name = "New Pet",
                DateOfBirth = new DateTime(2010, 01, 01),
                OwnerId = Guid.NewGuid(),
                Species = "Dog"
            };

            _updatePetInteractor = new UpdatePetInteractor(_mockContext.Object, _mockMapper.Object);
        }

        #endregion

        #region Setup Method

        [SetUp]
        public void Setup()
        {
            _updatePetRequest.PetId = Guid.NewGuid();
            _updatePetRequest.OwnerId = Guid.NewGuid();

            _mockMapper
               .Setup(e => e.Map<Pet>(It.IsAny<UpdatePetRequest>()))
               .Returns(() => new Pet
               {
                   PetId = _updatePetRequest.PetId,
                   Name = _updatePetRequest.Name ?? string.Empty,
                   DateOfBirth = (DateTime)_updatePetRequest.DateOfBirth,
                   Owner = new Owner
                   {
                       OwnerId = _updatePetRequest.OwnerId,
                       Address = new Address
                       {
                           Country = "Country",
                           PostalCode = "PostalCode",
                           State = "State",
                           StreetAddress = "StreetAddress",
                           Suburb = "Suburb"
                       },
                       ContactDetails = new Contact(),
                       Name = "Owner Name"
                   },
                   Species = _updatePetRequest.Species ?? string.Empty
               });

            _mockContext
                .Setup(e => e.Get<Owner>())
                .Returns(new List<Owner>
                {
                    new Owner
                    {
                        OwnerId = _updatePetRequest.OwnerId,
                        Address = new Address
                        {
                            Country = "Country",
                            PostalCode = "PostalCode",
                            State = "State",
                            StreetAddress = "StreetAddress",
                            Suburb = "Suburb"
                        },
                        ContactDetails = new Contact(),
                        Name = "Owner Name"
                    }
                }.AsQueryable());

            _mockContext
                .Setup(e => e.Get<Pet>())
                .Returns(new List<Pet>
                {
                    new Pet
                    {
                        PetId = _updatePetRequest.PetId,
                        Name = "Old Pet",
                        DateOfBirth = new DateTime(2010, 01, 01),
                        Owner = new Owner
                        {
                            OwnerId = _updatePetRequest.OwnerId,
                            Address = new Address
                            {
                                Country = "Country",
                                PostalCode = "PostalCode",
                                State = "State",
                                StreetAddress = "StreetAddress",
                                Suburb = "Suburb"
                            },
                            ContactDetails = new Contact(),
                            Name = "Owner Name"
                        },
                        Species = "Dog"
                    }
                }.AsQueryable());
        }

        #endregion

        #region Interactor Tests

        [Test]
        public async Task UpdatingPet_UpdatesExistingPetToContextAsync()
        {
            await _updatePetInteractor.Handle(_updatePetRequest, CancellationToken.None);
            var updatedPet = _mockContext.Object.Get<Pet>().First();
            updatedPet.Name = "Cool Pet";

            Assert.That(updatedPet.Name, Is.EqualTo("Cool Pet"));
        }

        [Test]
        public void UpdatingPet_ThrowsExceptionWhenOwnerNotFound()
        {
            _updatePetRequest.OwnerId = Guid.NewGuid();
            Assert.ThrowsAsync<Exception>(() => _updatePetInteractor.Handle(_updatePetRequest, CancellationToken.None));
        }

        [Test]
        public void UpdatingPet_ThrowsExceptionWhenPetNotFound()
        {
            _updatePetRequest.PetId = Guid.NewGuid();
            Assert.ThrowsAsync<Exception>(() => _updatePetInteractor.Handle(_updatePetRequest, CancellationToken.None));
        }

        #endregion
    }

}
