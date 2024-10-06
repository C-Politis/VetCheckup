namespace VetCheckup.Domain.Entities;

public class Owner
{

    #region Properties

    public required Address Address { get; set; }

    public required Contact ContactDetails { get; set; }

    public DateTime DateOfBirth { get; set; }

    public required string Name { get; set; }

    public Guid OwnerId { get; set; }

    #endregion

}
