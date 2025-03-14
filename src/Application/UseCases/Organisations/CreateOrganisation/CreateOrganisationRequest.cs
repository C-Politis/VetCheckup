using VetCheckup.Application.Common.EntityRequests;
using VetCheckup.Domain.Enums;

namespace VetCheckup.Application.UseCases.Organisations.CreateOrganisation;

public class CreateOrganisationRequest : IRequest
{

    #region Properties

    public string? Abn { get; set;
    }
    public required CreateAddressRequest Address { get; set; }

    public required CreateContactRequest ContactDetails { get; set; }

    public required string Name { get; set; }

    public required OrganisationType OrganisationType { get; set; }

    #endregion

}
