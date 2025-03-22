using VetCheckup.Application.Dtos;

namespace VetCheckup.Application.UseCases.Organisations.GetOrganisation
{
    public class GetOrganisationRequest : IRequest<OrganisationDto>
    {

        #region Properties

        public required Guid OrganisationId { get; set; }

        #endregion

    }
}
