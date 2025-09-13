using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.OrganisationManagers.CreateOrganisationManager;

public class CreateOrganisationManagerProfile : Profile
{
    #region Constructors

    public CreateOrganisationManagerProfile()
    {
        _ = CreateMap<CreateOrganisationManagerRequest, OrganisationManager>()
            .ForMember(destination => destination.OrganisationManagerId, source => source.Ignore())
            .ForMember(destination => destination.Organisation, source => source.Ignore());
    }

    #endregion

}
