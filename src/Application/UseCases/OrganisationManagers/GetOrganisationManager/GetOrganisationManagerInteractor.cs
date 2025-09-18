using VetCheckup.Application.Dtos;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.OrganisationManagers.GetOrganisationManager
{
    public class GetOrganisationManagerInteractor(IDbContext dbContext, IMapper mapper) : IRequestHandler<GetOrganisationManagerRequest, OrganisationManagerDto>
    {
        #region Methods

        Task<OrganisationManagerDto> IRequestHandler<GetOrganisationManagerRequest, OrganisationManagerDto>.Handle(GetOrganisationManagerRequest request, CancellationToken cancellationToken)
        {
            var organisationManager = dbContext.Get<OrganisationManager>().SingleOrDefault(o => o.OrganisationManagerId == request.OrganisationManagerId) ?? throw new Exception($"Organisation Manager not found.");

            return Task.FromResult(mapper.Map<OrganisationManagerDto>(organisationManager));
        }

        #endregion
    }
}
