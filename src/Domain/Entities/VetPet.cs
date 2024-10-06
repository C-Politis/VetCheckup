namespace VetCheckup.Domain.Entities;

public class VetPet
{

    #region Properties

    public required Pet Pet { get; set; }

    public required Vet Vet { get; set; }

    #endregion

}
