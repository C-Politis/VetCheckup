using VetCheckup.Application.Common.EntityRequests;
using VetCheckup.Application.UseCases.Organisations.CreateOrganisation;
using VetCheckup.Domain.Enums;

namespace VetCheckup.Application.UseCases.OrganisationManagers.CreateOrganisationManager;

public class CreateOrganisationManagerRequest : IRequest
{

    #region Properties

    public required CreateAddressRequest Address { get; set; }

    public required CreateContactRequest ContactDetails { get; set; }

    public required DateTime DateOfBirth { get; set; } // Conflicts with requeirement in spec: Configs currently require DOB but spec wants it nullable

    public required Title Title { get; set; }

    public required string FirstName { get; set; }

    public required string MiddleName { get; set; }

    public required string LastName { get; set; }

    public required Suffix Suffix { get; set; }

    public CreateOrganisationRequest? Organisation { get; set; }

    #endregion
}
