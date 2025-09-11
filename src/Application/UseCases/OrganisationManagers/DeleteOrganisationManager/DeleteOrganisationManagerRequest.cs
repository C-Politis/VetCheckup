using VetCheckup.Application.Dtos;

namespace VetCheckup.Application.UseCases.OrganisationManagers.DeleteOrganisationManager
{
    public class DeleteOrganisationManagerRequest : IRequest
    {

        #region Properties

        public required Guid OrganisationManagerId { get; set; }

        #endregion

    }
}
