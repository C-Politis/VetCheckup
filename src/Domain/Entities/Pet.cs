namespace VetCheckup.Domain.Entities;

public class Pet
{

    #region Properties

    public DateOnly DateOfBirth { get; set; }

    public int? MicrochipId { get; set; }

    public required string Name { get; set; }

    public required Owner Owner { get; set; }

    public Guid PetId { get; set; }

    public Sex Sex { get; set; }

    public required string Species { get; set; }

    #endregion

}
