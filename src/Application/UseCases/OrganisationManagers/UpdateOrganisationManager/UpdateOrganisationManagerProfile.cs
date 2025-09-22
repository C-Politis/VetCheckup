using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.OrganisationManagers.UpdateOrganisationManager;

public class UpdateOrganisationManagerProfile : Profile
{

    #region Constructors

    public UpdateOrganisationManagerProfile()
    {
        _ = this.CreateMap<UpdateOrganisationManagerRequest, OrganisationManager>()
            .ForMember(dest => dest.OrganisationManagerId, opts => opts.Ignore())
            .ForMember(dest => dest.User, opts => opts.Ignore())
            .ForMember(dest => dest.Organisation, opts => opts.Ignore());
    }

    #endregion
    
}
