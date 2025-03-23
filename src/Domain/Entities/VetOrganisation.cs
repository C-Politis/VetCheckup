namespace VetCheckup.Domain.Entities;

public class VetOrganisation
{
    
    #region Properties

    public required Vet Vet { get; set; } 
    
    public required Organisation Organisation { get; set; }

    public required bool IsPrimaryOrganisation { get; set; }

    #endregion
    
}
