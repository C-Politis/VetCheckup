using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Vets.UpdateVet;

public class UpdateVetInteractor(IDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateVetRequest>
{

    #region Methods

    Task IRequestHandler<UpdateVetRequest>.Handle(UpdateVetRequest request, CancellationToken cancellationToken)
    {
        var vet = dbContext.Get<Vet>().SingleOrDefault(v => v.VetId == request.VetId) ?? throw new Exception("Vet not found.");
        var organisations = dbContext.Get<Organisation>().Where(o => request.OrganisationIds != null && request.OrganisationIds.Contains(o.OrganisationId)).ToList();
        var existingOrganisationIds = organisations.Select(o => o.OrganisationId).ToList();
        if (request.OrganisationIds != null)
        {
            var nonExistentOrganisationIds = request.OrganisationIds.Except(existingOrganisationIds).ToList();
           
            if (nonExistentOrganisationIds.Any())
                throw new Exception($"The following OrganisationIds do not exist: {string.Join(", ", nonExistentOrganisationIds)}");
        }
           
        vet.VetOrganisations = organisations.Select(o => new VetOrganisation
        {
            Organisation = o,
            Vet = vet,
            IsPrimaryOrganisation = o.OrganisationId == request.PrimaryOrganisationId
        }).ToList();

        _ = mapper.Map(request, vet);    

        return Task.CompletedTask;
    }

    #endregion

}
