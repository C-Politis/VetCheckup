namespace VetCheckup.Domain.Entities;

public class Organisation
{

    #region Properties

    public string? Abn { get; set; }

    public required Address Address { get; set; }

    public required Contact ContactDetails { get; set; }

    public required string Name { get; set; }

    public Guid OrganisationId { get; set; }

    public required OrganisationType OrganisationType { get; set; } 

    #endregion

}

