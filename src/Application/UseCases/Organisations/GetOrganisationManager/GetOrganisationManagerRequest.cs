using VetCheckup.Application.Dtos;

namespace VetCheckup.Application.UseCases.Organisations.GetOrganisationManager
{
    public class GetOrganisationManagerRequest : IRequest<OrganisationManagerDto>
    {

        #region Properties

        public required Guid OrganisationManagerId { get; set; }

        #endregion

    }
}
