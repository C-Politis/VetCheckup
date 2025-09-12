using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.OrganisationManagers.DeleteOrganisationManager;
public class DeleteOrganisationManagerInteractor(IDbContext dbContext) : IRequestHandler<DeleteOrganisationManagerRequest>
{

    #region Methods

    Task IRequestHandler<DeleteOrganisationManagerRequest>.Handle(DeleteOrganisationManagerRequest request, CancellationToken cancellationToken)
    {
        var organisationManager = dbContext.Get<OrganisationManager>().SingleOrDefault(o => o.OrganisationManagerId == request.OrganisationManagerId) ?? throw new Exception($"OrganisationManager not found.");
        dbContext.Remove<OrganisationManager>(organisationManager);

        return Task.CompletedTask;
    }

    #endregion

}
