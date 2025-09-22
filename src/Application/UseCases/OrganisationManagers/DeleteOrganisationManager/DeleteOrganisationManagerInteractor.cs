using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.OrganisationManagers.DeleteOrganisationManager;
public class DeleteOrganisationManagerInteractor(IApplicationDbContext applicationDbContext) : IRequestHandler<DeleteOrganisationManagerRequest>
{

    #region Methods

    Task IRequestHandler<DeleteOrganisationManagerRequest>.Handle(DeleteOrganisationManagerRequest request, CancellationToken cancellationToken)
    {
        var organisationManager = applicationDbContext.Get<OrganisationManager>().SingleOrDefault(o => o.OrganisationManagerId == request.OrganisationManagerId) ?? throw new Exception($"OrganisationManager not found.");
        applicationDbContext.Remove<OrganisationManager>(organisationManager);

        return Task.CompletedTask;
    }

    #endregion

}
