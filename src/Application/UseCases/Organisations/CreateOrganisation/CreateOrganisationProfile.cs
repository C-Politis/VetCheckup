using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Organisations.CreateOrganisation;

public class CreateOrganisationProfile : Profile
{
    #region Constructors

    public CreateOrganisationProfile()
    {
        _ = CreateMap<CreateOrganisationRequest, Organisation>()
            .ForMember(destination => destination.OrganisationId, source => source.Ignore());
    }

    #endregion

}
