using Microsoft.AspNetCore.Identity;

namespace VetCheckup.Domain.Entities;

public class User : IdentityUser<Guid>
{

    #region Properties

    public required Roles Role { get; set; }
    
    #endregion

}

