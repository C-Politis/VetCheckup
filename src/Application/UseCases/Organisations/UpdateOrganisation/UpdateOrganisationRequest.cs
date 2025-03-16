using VetCheckup.Domain.Entities;
using VetCheckup.Domain.Enums;

namespace VetCheckup.Application.UseCases.Organisations.UpdateOrganisation;

public class UpdateOrganisationRequest : IRequest
{

    #region Properties

    public required Guid OrganisationId { get; set; }

    public string? Abn { get; set; }

    public Address? Address { get; set; }

    public Contact? ContactDetails { get; set; }

    public string? Name { get; set; }

    public OrganisationType? OrganisationType { get; set; }

    #endregion

}
