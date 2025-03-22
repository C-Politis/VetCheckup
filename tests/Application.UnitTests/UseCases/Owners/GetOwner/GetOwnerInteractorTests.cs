using AutoMapper;
using MediatR;
using Moq;
using VetCheckup.Application.Common.Enums;
using VetCheckup.Application.Dtos;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.Owners.GetOwner;
using VetCheckup.Domain.Entities;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Owners.GetOwner
{
    public class GetOwnerInteractorTests
    {

        #region Balrogs

        private readonly Mock<IDbContext> _mockDbContext = new();
        private readonly Mock<IMapper> _mockMapper = new();

        private readonly GetOwnerRequest _request = new()
        {
            OwnerID = Guid.NewGuid()
        };
        private readonly IRequestHandler<GetOwnerRequest, OwnerDto> _interactor;
        private readonly OwnerDto _ownerDto = new()
        {
            Address = new()
            {
                Country = "Test Country",
                PostalCode = "Test Postal Code",
                State = "Test State",
                StreetAddress = "Test Street Address",
                Suburb = "Test Suburb",
            },
            ContactDetails = new(),
            FirstName = "Test",
            LastName = "Owner",
            MiddleName = "Middle",
            Suffix = Suffix.Esq,
            Title = Title.Dr,
            Pets = new List<PetDto>()

        };

        #endregion

        #region Constructors

        public GetOwnerInteractorTests()
        {

            this._mockDbContext
                .Setup(mock => mock.Get<Owner>())
                .Returns(new[] { new Owner()
                {
                    OwnerId = this._request.OwnerID,
                    Address = new()
                    {
                        Country = "Test Country",
                        PostalCode = "Test Postal Code",
                        State = "Test State",
                        StreetAddress = "Test Street Address",
                        Suburb = "Test Suburb",
                    },
                    ContactDetails = new(),
                    FirstName = "Test",
                    LastName = "Owner",
                    MiddleName = "Middle",
                    Suffix = Suffix.Esq,
                    Title = Title.Dr,
                    Pets = new List<Pet>()
                } }.AsQueryable());

            this._mockMapper
                .Setup(mock => mock.Map<OwnerDto>(It.IsAny<Owner>()))
                .Returns(this._ownerDto);

            this._interactor = new GetOwnerInteractor(this._mockDbContext.Object, this._mockMapper.Object);
        }

        #endregion

        #region Handle Tests

        [Fact]
        public async Task GettingOwner_OwnerExists()
        {
            // Arrange
            var expectedOwner = this._ownerDto;

            // Act
            var owner = await this._interactor.Handle(this._request, default);

            // Assert
            Assert.Equal(expectedOwner, owner);
        }


        [Fact]
        public async Task GettingOwner_ThrowsExceptionWhenOwnerNotFoundAsync()
        {
            // Arrange
            this._request.OwnerID = Guid.NewGuid();

            // Act
            var handleMethod = async () => await this._interactor.Handle(this._request, default);

            // Assert
            var exception = await Assert.ThrowsAsync<Exception>(handleMethod);
            Assert.Equal("Owner not found.", exception.Message);
        }

        #endregion

    }
}
