using VetCheckup.Application.Common.EntityRequests;
using VetCheckup.Domain.Entities;
using VetCheckup.Domain.Enums;

namespace VetCheckup.Application.UseCases.OrganisationManagers.UpdateOrganisationManager;

public class UpdateOrganisationManagerRequest : IRequest
{
    
    #region Properties

    public Guid OrganisationManagerId { get; set; }
    
    public UpdateAddressRequest? Address { get; set; }

    public UpdateContactRequest? ContactDetails { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public Title? Title { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public Suffix? Suffix { get; set; }

    #endregion
    
}
