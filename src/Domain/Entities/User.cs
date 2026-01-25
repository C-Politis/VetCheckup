using Microsoft.AspNetCore.Identity;

namespace VetCheckup.Domain.Entities;

public class User : IdentityUser<Guid>
{

    #region Properties

    public required UserType UserType { get; set; }
    
    #endregion

}

