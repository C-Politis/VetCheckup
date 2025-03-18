using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Organisations.UpdateOrganisation;

public class UpdateOrganisationProfile : Profile
{

    #region Constructors

    public UpdateOrganisationProfile()
    {
        _ = this.CreateMap<UpdateOrganisationRequest, Organisation>()
            .ForMember(dest => dest.Abn, opts =>
            {
                opts.PreCondition(src => src.Abn != null);
                opts.MapFrom(src => src.Abn);
            })
            .ForMember(dest => dest.Name, opts =>
            {
                opts.PreCondition(src => src.Name != null);
                opts.MapFrom(src => src.Name);
            })
            .ForMember(dest => dest.OrganisationType, opts =>
            {
                opts.PreCondition(src => src.OrganisationType != null);
                opts.MapFrom(src => src.OrganisationType);
            })
            .ForMember(dest => dest.OrganisationId, opts => opts.Ignore());
    }
            
    #endregion

}
