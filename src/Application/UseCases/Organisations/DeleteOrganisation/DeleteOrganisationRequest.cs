namespace VetCheckup.Application.UseCases.Organisations.DeleteOrganisation;

public class DeleteOrganisationRequest : IRequest
{

    #region Properties

    public Guid OrganisationId { get; set; }

    #endregion
}
