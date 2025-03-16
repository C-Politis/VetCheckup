using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Organisations.UpdateOrganisation;

public class UpdateOrganisationProfile : Profile
{

    #region Constructors

    public UpdateOrganisationProfile()
        => CreateMap<UpdateOrganisationRequest, Organisation>();

    #endregion

}
