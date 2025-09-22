using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Organisations.DeleteOrganisation;

public class DeleteOrganisationInteractor(IApplicationDbContext context) : IRequestHandler<DeleteOrganisationRequest>
{

    #region Methods

    Task IRequestHandler<DeleteOrganisationRequest>.Handle(DeleteOrganisationRequest request, CancellationToken cancellationToken)
    {
        var organisation = context.Get<Organisation>().SingleOrDefault(e => e.OrganisationId == request.OrganisationId) ?? throw new Exception("Organisation not found");
        context.Remove(organisation);
        return Task.CompletedTask;
    }

    #endregion

}
