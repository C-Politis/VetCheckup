using VetCheckup.Application.Common.EntityRequests;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.Common.Mappings;
public class EntityRequestProfile : Profile
{

    #region Constructors

    public EntityRequestProfile()
    {
        _ = this.CreateMap<CreateAddressRequest, Address>()
            .ForMember(destination => destination.AddressId, source => source.Ignore())
            .ForMember(destination => destination.Country, source => source.MapFrom(e => e.Country ?? string.Empty))
            .ForMember(destination => destination.PostalCode, source => source.MapFrom(e => e.PostalCode ?? string.Empty))
            .ForMember(destination => destination.State, source => source.MapFrom(e => e.State ?? string.Empty))
            .ForMember(destination => destination.StreetAddress, source => source.MapFrom(e => e.StreetAddress ?? string.Empty))
            .ForMember(destination => destination.Suburb, source => source.MapFrom(e => e.Suburb ?? string.Empty));

        _ = this.CreateMap<CreateContactRequest, Contact>()
            .ForMember(destination => destination.ContactId, source => source.Ignore());

    }

    #endregion

}
