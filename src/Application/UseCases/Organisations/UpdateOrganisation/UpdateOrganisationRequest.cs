using VetCheckup.Application.Common.EntityRequests;
using VetCheckup.Domain.Entities;
using VetCheckup.Domain.Enums;

namespace VetCheckup.Application.UseCases.Organisations.UpdateOrganisation;

public class UpdateOrganisationRequest : IRequest
{

    #region Properties

    public required Guid OrganisationId { get; set; }

    public string? Abn { get; set; }

    public string? Name { get; set; }

    public OrganisationType? OrganisationType { get; set; }

    public required UpdateAddressRequest Address { get; set; }

    public required UpdateContactRequest ContactDetails { get; set; }
    
    public List<Guid>? VetIds { get; set; }
    
    #endregion

}
