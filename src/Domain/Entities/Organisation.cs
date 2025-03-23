namespace VetCheckup.Domain.Entities;

public class Organisation
{

    #region Properties

    public required string Abn { get; set; }

    public required Address Address { get; set; }

    public required Contact ContactDetails { get; set; }

    public required string Name { get; set; }

    public required Guid OrganisationId { get; set; }

    public required OrganisationType OrganisationType { get; set; }

    public required ICollection<VetOrganisation> VetOrganisations { get; set; }
    
    #endregion

}

