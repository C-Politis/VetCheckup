namespace VetCheckup.Domain.Entities;

public class Owner
{

    #region Properties

    public required User User { get; set; }

    public required Address Address { get; set; }

    public required Contact ContactDetails { get; set; }

    public required DateTime DateOfBirth { get; set; }

    public required Title Title { get; set; }
    
    public required string FirstName { get; set; }

    public required string MiddleName { get; set; }

    public required string LastName { get; set; }

    public required Suffix Suffix { get; set; }

    public required Guid OwnerId { get; set; }

    public required ICollection<Pet> Pets { get; set; }

    #endregion

}
