using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Moq;
using NUnit.Framework;
using VetCheckup.Application.Dtos;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.Owners.GetOwner;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UnitTests.UseCases.Owners.GetOwner
{
    public class GetOwnerInteractorTests
    {

        #region Properties

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
            Name = "Test Owner",
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
                    Name = "Test Owner",
                } }.AsQueryable());

            this._mockMapper
                .Setup(mock => mock.Map<OwnerDto>(It.IsAny<Owner>()))
                .Returns(this._ownerDto);

            this._interactor = new GetOwnerInteractor(this._mockDbContext.Object, this._mockMapper.Object);
        }

        #endregion

        #region Handle Tests

        [Test]
        public async Task GettingOwner_OwnerExists()
        {
            var owner = await this._interactor.Handle(this._request, default);

            Assert.That(owner, Is.EqualTo(this._ownerDto));
        }

        [Test]
        public void GettingOwner_ThrowsExceptionWhenOwnerNotFound()
        {
            this._request.OwnerID = Guid.NewGuid();

            var handleMethod = async () => await this._interactor.Handle(this._request, default);
                
            Assert.ThrowsAsync<Exception>(() => handleMethod(), "Owner not found.");
        }

        #endregion

    }
}
