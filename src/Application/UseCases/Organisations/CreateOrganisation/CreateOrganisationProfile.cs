using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Organisations.CreateOrganisation;

public class CreateOrganisationProfile : Profile
{
    #region Constructors

    public CreateOrganisationProfile()
    {
        _ = CreateMap<CreateOrganisationRequest, Organisation>()
            .ForMember(destination => destination.OrganisationId, source => source.Ignore())
            .ForMember(destination => destination.VetOrganisations, source => source.Ignore())
            .ForMember(destination => destination.OrganisationManager, source => source.Ignore()); // Circular relationship fucks the mapping a bit.
                                                                                                   // Im assuming create Org is called by Create OrgManager and not other way around
    }

    #endregion

}
