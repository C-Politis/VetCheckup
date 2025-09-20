using VetCheckup.Domain.Enums;

namespace VetCheckup.Application.UseCases.Users.UpdateUser;

public class UpdateUserRequest : IRequest
{

    #region Properties
    
    public string? Username { get; set; }
    
    public string? Password { get; set; }
    
    public UserType UserType { get; set; }
    
    public required string Email { get; set; }

    #endregion
    
}
