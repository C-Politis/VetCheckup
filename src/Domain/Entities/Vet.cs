namespace VetCheckup.Domain.Entities;

public class Vet
{

    #region Properties
    
    public DateTime DateOfBirth { get; set; }

    public required string Name { get; set; }

    public Guid VetId { get; set; } 

    #endregion

}
