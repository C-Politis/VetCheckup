using VetCheckup.Domain.Enums;

namespace VetCheckup.Application.Common.EntityRequests;

public class CreateUserRequest
{
    #region Properties

    public required string UserName { get; set; }

    public required string Password { get; set; }

    public required UserType UserType { get; set; }

    #endregion


}
