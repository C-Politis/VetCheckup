using VetCheckup.Application.Dtos;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Organisations.GetOrganisation
{
    public class GetOrganisationInteractor(IApplicationDbContext applicationDbContext, IMapper mapper) : IRequestHandler<GetOrganisationRequest, OrganisationDto>
    {

        #region Methods

        Task<OrganisationDto> IRequestHandler<GetOrganisationRequest, OrganisationDto>.Handle(GetOrganisationRequest request, CancellationToken cancellationToken)
        {
            var organisation = applicationDbContext.Get<Organisation>().SingleOrDefault(o => o.OrganisationId == request.OrganisationId) ?? throw new Exception($"Organisation not found.");

            return Task.FromResult(mapper.Map<OrganisationDto>(organisation));
        }

        #endregion

    }
}
