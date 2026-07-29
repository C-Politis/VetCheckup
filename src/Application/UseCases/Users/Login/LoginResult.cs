using VetCheckup.Domain.Enums;

namespace VetCheckup.Application.UseCases.Users.Login;

public class LoginResult
{
    public required Guid Id { get; set; }

    public required string UserName { get; set; }

    public required Roles Role { get; set; }
}
