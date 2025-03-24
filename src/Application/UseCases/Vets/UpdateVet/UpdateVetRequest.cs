using VetCheckup.Application.Common.EntityRequests;
using VetCheckup.Application.Common.Enums;

namespace VetCheckup.Application.UseCases.Vets.UpdateVet;

public class UpdateVetRequest : IRequest
{

    #region Properties
    public required Guid VetId { get; set; }

    public required UpdateAddressRequest Address { get; set; }
    
    public required UpdateContactRequest ContactDetails { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public Title? Title { get; set; }

    public Suffix? Suffix { get; set; }

    public List<Guid>? OrganisationIds { get; set; }

    public Guid? PrimaryOrganisationId { get; set; }

    #endregion

}
