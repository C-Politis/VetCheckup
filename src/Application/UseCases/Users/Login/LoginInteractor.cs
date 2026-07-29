using VetCheckup.Application.Common.Interfaces;

namespace VetCheckup.Application.UseCases.Users.Login;

public class LoginInteractor(IAuthenticationService authenticationService) : IRequestHandler<LoginRequest, LoginResult>
{
    public async Task<LoginResult> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await authenticationService.AuthenticateAsync(request.UserName, request.Password);

        if (user is null)
        {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        return new LoginResult
        {
            Id = user.Id,
            UserName = user.UserName ?? string.Empty,
            Role = user.Role
        };
    }
}
