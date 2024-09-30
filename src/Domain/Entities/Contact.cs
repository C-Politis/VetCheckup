namespace VetCheckup.Domain.Entities;

public class Contact
{
    
    #region Properties
    
    public int ContactId { get; set; }

    public required string Email { get; set; }

    public int? Mobile { get; set; } 
    
    #endregion

}
