using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Vets.CreateVet;

public class CreateVetInteractor(IApplicationDbContext context, IMapper mapper) : IRequestHandler<CreateVetRequest>
{

    #region Methods

    Task IRequestHandler<CreateVetRequest>.Handle(CreateVetRequest request, CancellationToken cancellationToken)
    {
        var organisations = context.Get<Organisation>().Where(o => request.OrganisationIds != null && request.OrganisationIds.Contains(o.OrganisationId)).ToList();
        var existingOrganisationIds = organisations.Select(o => o.OrganisationId).ToList();
        
        if (request.OrganisationIds != null)
        {
            var nonExistentOrganisationIds = request.OrganisationIds.Except(existingOrganisationIds).ToList();
           
            if (nonExistentOrganisationIds.Any())
                throw new Exception($"The following OrganisationIds do not exist: {string.Join(", ", nonExistentOrganisationIds)}");
        }

        var vet = mapper.Map<Vet>(request);
        
        vet.VetOrganisations = organisations.Select(o => new VetOrganisation
        {
            Organisation = o,
            Vet = vet,
            IsPrimaryOrganisation = request.PrimaryOrganisationId == o.OrganisationId
        }).ToList();
        
        context.Add(vet);

        return Task.CompletedTask;
    }

    #endregion

}
