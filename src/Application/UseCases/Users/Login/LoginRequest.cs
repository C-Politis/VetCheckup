namespace VetCheckup.Application.UseCases.Users.Login;

public class LoginRequest : IRequest<LoginResult>
{
    #region Properties

    public required string UserName { get; set; }

    public required string Password { get; set; }

    #endregion
}
