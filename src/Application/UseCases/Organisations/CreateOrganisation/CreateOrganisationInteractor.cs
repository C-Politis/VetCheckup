using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Organisations.CreateOrganisation;
public class CreateOrganisationInteractor(IApplicationDbContext context, IMapper mapper) : IRequestHandler<CreateOrganisationRequest>
{
    
    #region Methods

    Task IRequestHandler<CreateOrganisationRequest>.Handle(CreateOrganisationRequest request, CancellationToken cancellationToken)
    {
        context.Add(mapper.Map<Organisation>(request));

        return Task.CompletedTask;
    }
    
    #endregion
}
