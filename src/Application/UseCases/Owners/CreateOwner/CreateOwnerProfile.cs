using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Owners.CreateOwner;
public class CreateOwnerProfile : Profile
{
    #region Constructors

    public CreateOwnerProfile()
    {
        _ = CreateMap<CreateOwnerRequest, Owner>()
            .ForMember(destination => destination.OwnerId, source => source.Ignore());
    }

    #endregion

}
