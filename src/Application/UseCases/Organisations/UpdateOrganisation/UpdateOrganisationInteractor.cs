using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Organisations.UpdateOrganisation;

public class UpdateOrganisationInteractor(IDbContext context, IMapper mapper) : IRequestHandler<UpdateOrganisationRequest>
{

    #region Methods

    Task IRequestHandler<UpdateOrganisationRequest>.Handle(UpdateOrganisationRequest request, CancellationToken cancellationToken)
    {
        var organisation = context.Get<Organisation>().FirstOrDefault(org => org.OrganisationId == request.OrganisationId) ?? throw new Exception("Organisation not found.");
        mapper.Map(request, organisation);

        return Task.CompletedTask;
    }

    #endregion

}
