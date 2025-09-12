using VetCheckup.Application.Services.Persistence;
using VetCheckup.Application.UseCases.OrganisationManagers.CreateOrganisationManager;
using VetCheckup.Domain.Entities;

public class CreateOrganisationManagerInteractor(IDbContext context, IMapper mapper) : IRequestHandler<CreateOrganisationManagerRequest>
{

    #region Methods

    Task IRequestHandler<CreateOrganisationManagerRequest>.Handle(CreateOrganisationManagerRequest request, CancellationToken cancellationToken)
    {
        context.Add(mapper.Map<OrganisationManager>(request));

        return Task.CompletedTask;
    }

    #endregion
}
