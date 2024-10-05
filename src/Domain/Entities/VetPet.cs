namespace VetCheckup.Domain.Entities;

public class VetPet
{

    #region Properties

    public required Pet Pet { get; set; }

    public required Vet VetId { get; set; }

    #endregion

}
