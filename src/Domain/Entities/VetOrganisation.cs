namespace VetCheckup.Domain.Entities;

public class VetOrganisation
{
    
    public required Guid VetId { get; set; }
    public Vet? Vet { get; set; }

    public required Guid OrganisationId { get; set; }
    public Organisation? Organisation { get; set; }

    public required bool IsPrimaryOrganisation { get; set; }
}
