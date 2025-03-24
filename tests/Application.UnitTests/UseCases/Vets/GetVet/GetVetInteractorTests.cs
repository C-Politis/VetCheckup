using AutoMapper;
using MediatR;
using Moq;
using VetCheckup.Application.Dtos;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.Vets.GetVet;
using VetCheckup.Domain.Entities;
using VetCheckup.Domain.Enums;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Vets.GetVet
{
    public class GetVetInteractorTests
    {

        // https://www.youtube.com/watch?v=0Z9rCMjEmfY
        #region A MINE!

        private readonly Mock<IDbContext> _mockDbContext = new();
        private readonly Mock<IMapper> _mockMapper = new();

        private readonly GetVetRequest _request;
        private readonly IRequestHandler<GetVetRequest, VetDto> _interactor;
        private readonly VetDto _vetDto;

        #endregion

        #region Constructors

        public GetVetInteractorTests()
        {
            _request = new()
            {
                VetId = Guid.NewGuid()
            };

            _vetDto = new()
            {
                VetId = _request.VetId,
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
                Title = Title.Mr,
                FirstName = "Gimli",
                MiddleName = "son of",
                LastName = "Gloin",
                Suffix = Suffix.None,
                DateOfBirth = DateTime.MinValue,
                VetOrganisations = new List<VetOrganisation>()
            };

            this._mockDbContext
                .Setup(mock => mock.Get<Vet>())
                .Returns(new[] { new Vet()
                    {
                        VetId = _request.VetId,
                        Address = _vetDto.Address,
                        ContactDetails = _vetDto.ContactDetails,
                        Title = Title.Mr,
                        FirstName = "Gimli",
                        MiddleName = "son of",
                        LastName = "Gloin",
                        Suffix = Suffix.None,
                        DateOfBirth = DateTime.MinValue,
                        VetOrganisations = new List<VetOrganisation>()
                    }
                }.AsQueryable());

            this._mockMapper
                .Setup(mock => mock.Map<VetDto>(It.IsAny<Vet>()))
                .Returns(this._vetDto);

            this._interactor = new GetVetInteractor(this._mockDbContext.Object, this._mockMapper.Object);
        }

        #endregion

        #region Interactor Tests

        [Fact]
        public async Task GettingVet_VetExists()
        {
            // Arrange
            var expectedVet = this._vetDto;

            // Act
            var vet = await this._interactor.Handle(this._request, default);

            // Assert
            Assert.Equal(expectedVet, vet);
        }

        [Fact]
        public async Task GettingVet_ThrowsExceptionWhenVetNotFound()
        {
            // Arrange
            this._request.VetId = Guid.NewGuid();

            // Act
            var handleMethod = async () => await this._interactor.Handle(this._request, default);

            // Assert
            await Assert.ThrowsAsync<Exception>(handleMethod);
        }

        #endregion

    }
}
