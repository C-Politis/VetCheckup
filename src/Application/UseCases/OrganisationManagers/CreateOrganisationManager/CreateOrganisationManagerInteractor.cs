using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.OrganisationManagers.CreateOrganisationManager;
using VetCheckup.Domain.Entities;

public class CreateOrganisationManagerInteractor(IDbContext context, IMapper mapper) : IRequestHandler<CreateOrganisationManagerRequest>
{

    #region Methods

    Task IRequestHandler<CreateOrganisationManagerRequest>.Handle(CreateOrganisationManagerRequest request, CancellationToken cancellationToken)
    {

        var manager = mapper.Map<OrganisationManager>(request);

        if (manager.Organisation != null)
        {
            manager.Organisation.OrganisationManager = manager;
        }

        context.Add(manager);

        return Task.CompletedTask;
    }

    #endregion
}
