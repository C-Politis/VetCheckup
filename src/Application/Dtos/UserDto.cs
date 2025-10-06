using VetCheckup.Domain.Enums;

namespace VetCheckup.Application.Dtos;

public class UserDto
{

    #region Properties

    public required string UserName { get; set; }

    public required string Password { get; set; }

    public required String Id { get; set; }

    #endregion

}

