﻿using AutoMapper;
using MediatR;
using Moq;
using VetCheckup.Application.Dtos;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.Organisations.GetOrganisation;
using VetCheckup.Domain.Enums;
using VetCheckup.Domain.Entities;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Organisations.GetOrganisation
{
    public class GetOrganisationInteractorTests
    {

        #region Pipeweed Ganjalf

        private readonly Mock<IDbContext> _mockDbContext = new();
        private readonly Mock<IMapper> _mockMapper = new();

        private readonly GetOrganisationRequest _request;
        private readonly IRequestHandler<GetOrganisationRequest, OrganisationDto> _interactor;
        private readonly OrganisationDto _organisationDto = new()
        {
            OrganisationId = Guid.NewGuid(),
            Abn = string.Empty,
            Address = new()
            {
                AddressId = Guid.NewGuid(),
                Country = "Test Country",
                PostalCode = "Test Postal Code",
                State = "Test State",
                StreetAddress = "Test Street Address",
                Suburb = "Test Suburb",
            },
            ContactDetails = new()
            {   
                ContactId = Guid.NewGuid(),
                Email = string.Empty,
                Mobile = string.Empty
            },
            Name = "Hobbiton Rescue",
            OrganisationType = OrganisationType.Other,
            VetOrganisations = new List<VetOrganisation>()
        };

        #endregion

        #region Constructors

        public GetOrganisationInteractorTests()
        {
            this._request = new() 
            { 
                OrganisationId = _organisationDto.OrganisationId
            };

            this._mockDbContext
                .Setup(mock => mock.Get<Organisation>())
                .Returns(new[] { new Organisation()
                {
                    OrganisationId = _organisationDto.OrganisationId,
                    Abn = string.Empty,
                    VetOrganisations = new List<VetOrganisation>(),
                    Address = new()
                    {
                        AddressId = _organisationDto.Address.AddressId,
                        Country = "Test Country",
                        PostalCode = "Test Postal Code",
                        State = "Test State",
                        StreetAddress = "Test Street Address",
                        Suburb = "Test Suburb",
                    },
                    ContactDetails = new()
                    {
                        ContactId = _organisationDto.ContactDetails.ContactId,
                        Email = string.Empty,
                        Mobile = string.Empty
                    },
                    Name = "Hobbiton Rescue",
                    OrganisationType = OrganisationType.Other
                } }.AsQueryable());

            this._mockMapper
                .Setup(mock => mock.Map<OrganisationDto>(It.IsAny<Organisation>()))
                .Returns(this._organisationDto);

            this._interactor = new GetOrganisationInteractor(this._mockDbContext.Object, this._mockMapper.Object);
        }

        #endregion

        #region Handle Tests

        [Fact]
        public async Task GettingOrganisation_OrganisationExists()
        {
            // Arrange
            var expectedOrganisation = this._organisationDto;

            // Act
            var organisation = await this._interactor.Handle(this._request, default);

            // Assert
            Assert.Equal(expectedOrganisation, organisation);
        }

        [Fact]
        public async Task GettingOrganisation_ThrowsExceptionWhenOrganisationNotFoundAsync()
        {
            // Arrange
            this._request.OrganisationId = Guid.NewGuid();

            // Act
            var handleMethod = async () => await this._interactor.Handle(this._request, default);

            // Assert
            var exception = await Assert.ThrowsAsync<Exception>(handleMethod);
            Assert.Equal("Organisation not found.", exception.Message);
        }

        #endregion

    }
}
