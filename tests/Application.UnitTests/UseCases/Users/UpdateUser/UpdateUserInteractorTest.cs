using AutoMapper;
using MediatR;
using Moq;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.Users.UpdateUser;
using VetCheckup.Domain.Entities;
using VetCheckup.Domain.Enums;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Users;

public class UpdateUserInteractorTest
{
    #region Fields

    private readonly Mock<IApplicationDbContext> _mockContext = new();
    private readonly Mock<IMapper> _mockMapper = new();

    private readonly IRequestHandler<UpdateUserRequest> _updateUserInteractor;
    private readonly UpdateUserRequest _updateUserRequest;

    #endregion

    #region Constructors

    public UpdateUserInteractorTest()
    {
        _updateUserRequest = new UpdateUserRequest()
        {
            Username = "newusername",
            UserType = UserType.OrganisationManager,
            Email = "bob@bobson.com",
            Password = "f4k3H4shV4lu3"
        };
        
        _mockContext
            .Setup(e => e.Get<OrganisationManager>())
            .Returns(new List<OrganisationManager>
            {
                new OrganisationManager
                {
                    FirstName = "Bob",
                    LastName = "Bobson",
                    DateOfBirth = DateTime.MaxValue,
                    OrganisationManagerId = Guid.NewGuid(),
                    Address = new Address
                    {
                        AddressId = Guid.NewGuid(),
                        Country = string.Empty,
                        PostalCode = string.Empty,
                        State = string.Empty,
                        StreetAddress = string.Empty,
                        Suburb = string.Empty
                    },
                    User = new User
                    {
                        UserId = Guid.NewGuid(),
                        UserName = string.Empty,
                        UserType = UserType.OrganisationManager,
                        Password = "Password"
                    },
                    MiddleName = string.Empty,
                    Suffix = Suffix.None,
                    ContactDetails = new Contact
                    {
                        Email = "bob@bobson.com",
                        Mobile = "",
                        ContactId = Guid.NewGuid(),
                    },
                    Title = Title.Dr
                }
            }.AsQueryable());
        
        _updateUserInteractor = new UpdateUserInteractor(this._mockContext.Object, this._mockMapper.Object);
    }

    #endregion


    #region Tests

    [Fact]
    public async Task UpdatingUser_ShouldUpdateUser()
    {
        // Act
        await _updateUserInteractor.Handle(_updateUserRequest, CancellationToken.None);

        // Assert
        _mockContext.Verify(mock => mock.Get<OrganisationManager>(), Times.Once);
        _mockMapper.Verify(mock => mock.Map(It.IsAny<UpdateUserRequest>(), It.IsAny<User>()), Times.Once);
    }


    [Fact]
    public async Task UpdatingUser_WithNonExistentEmail_ShouldThrowException()
    {
        // Arrange
        var invalidRequest = new UpdateUserRequest
        {
            Username = "newusername", UserType = UserType.OrganisationManager, Email = "", Password = ""
        };
        
        // Act & Assert
        await Assert.ThrowsAsync<Exception>(async () =>
            await _updateUserInteractor.Handle(invalidRequest, CancellationToken.None));
    }

    #endregion
    
    
}
