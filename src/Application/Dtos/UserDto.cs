using VetCheckup.Domain.Enums;

namespace VetCheckup.Application.Dtos;

public class UserDto
{

    #region Properties

    public required UserType UserType { get; set; }

    #endregion

}

