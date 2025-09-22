using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.OrganisationManagers.UpdateOrganisationManager;

public class UpdateOrganisationManagerInteractor(IApplicationDbContext applicationDbContext, IMapper mapper) : IRequestHandler<UpdateOrganisationManagerRequest>
{
    #region Methods

    Task IRequestHandler<UpdateOrganisationManagerRequest>.Handle(UpdateOrganisationManagerRequest request, CancellationToken cancellationToken)
    {
        var organisationManager = applicationDbContext.Get<OrganisationManager>()
            .SingleOrDefault(o => o.OrganisationManagerId == request.OrganisationManagerId) ?? throw new Exception($"Organisation Manager not found.");

        _ = mapper.Map(request, organisationManager);

        return Task.FromResult(organisationManager);
    }

    #endregion
}
