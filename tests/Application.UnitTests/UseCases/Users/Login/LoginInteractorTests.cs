using MediatR;
using Moq;
using VetCheckup.Application.Common.Interfaces;
using VetCheckup.Application.UseCases.Users.Login;
using VetCheckup.Domain.Entities;
using VetCheckup.Domain.Enums;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Users.Login;

public class LoginInteractorTests
{
    private readonly Mock<IAuthenticationService> _authenticationService = new();
    private readonly IRequestHandler<LoginRequest, LoginResult> _interactor;
    private readonly LoginRequest _request;
    private readonly User _user;

    public LoginInteractorTests()
    {
        _request = new LoginRequest
        {
            UserName = "VetUser",
            Password = "P@ssword123"
        };

        _user = new User
        {
            Id = Guid.NewGuid(),
            UserName = _request.UserName,
            Role = Roles.Vet
        };

        _authenticationService
            .Setup(service => service.AuthenticateAsync(_request.UserName, _request.Password))
            .ReturnsAsync(_user);

        _interactor = new LoginInteractor(_authenticationService.Object);
    }

    [Fact]
    public async Task Login_WithValidCredentials_ReturnsLoginResult()
    {
        var result = await _interactor.Handle(_request, CancellationToken.None);

        Assert.Equal(_user.Id, result.Id);
        Assert.Equal(_user.UserName, result.UserName);
        Assert.Equal(_user.Role, result.Role);
        _authenticationService.Verify(service => service.AuthenticateAsync(_request.UserName, _request.Password), Times.Once);
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ThrowsUnauthorizedAccessException()
    {
        _authenticationService
            .Setup(service => service.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((User?)null);

        var handleMethod = async () => await _interactor.Handle(_request, CancellationToken.None);

        await Assert.ThrowsAsync<UnauthorizedAccessException>(handleMethod);
    }
}
