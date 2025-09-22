using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Organisations.UpdateOrganisation;

public class UpdateOrganisationInteractor(IApplicationDbContext context, IMapper mapper) : IRequestHandler<UpdateOrganisationRequest>
{

    #region Methods

    Task IRequestHandler<UpdateOrganisationRequest>.Handle(UpdateOrganisationRequest request, CancellationToken cancellationToken)
    {
        var organisation = context.Get<Organisation>().FirstOrDefault(org => org.OrganisationId == request.OrganisationId) ?? throw new Exception("Organisation not found.");
        var vet = context.Get<Vet>().Where(v => request.VetIds != null && request.VetIds.Contains(v.VetId)).ToList();
        var existingVetIds = vet.Select(v => v.VetId).ToList();
        if (request.VetIds != null)
        {
            var nonExistentVetIds = request.VetIds.Except(existingVetIds).ToList();
            
            if (nonExistentVetIds.Any())
                throw new Exception($"The following VetIds do not exist: {string.Join(", ", nonExistentVetIds)}");
        }
        
        organisation.VetOrganisations = vet.Select(v => 
        {
            if (organisation.VetOrganisations.Any(vo => vo.Vet.VetId == v.VetId))
                return organisation.VetOrganisations.First(vo => vo.Vet.VetId == v.VetId);

            return new VetOrganisation
            {
                Organisation = organisation,
                Vet = v,
                IsPrimaryOrganisation = false
            };
        }).ToList();
        
        
        mapper.Map(request, organisation);

        return Task.CompletedTask;
    }

    #endregion

}
