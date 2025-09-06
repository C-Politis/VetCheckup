namespace VetCheckup.Domain.Entities;

public class OrganisationManager
{
    #region Properties

    public required Address Address { get; set; }
    
    public required Contact ContactDetails { get; set; }
    
    public required DateTime DateOfBirth { get; set; }
    
    public required Title Title { get; set; }

    public required string FirstName { get; set; }

    public required string MiddleName { get; set; }

    public required string LastName { get; set; }

    public required Suffix Suffix { get; set; }
    
    public required Organisation Organisation { get; set; }
    
    public required Guid OrganisationManagerId { get; set; }
    
    #endregion
}
