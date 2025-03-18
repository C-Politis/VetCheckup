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

        _ = this.CreateMap<UpdateAddressRequest, Address>()
            .ForMember(destination => destination.AddressId, source => source.Ignore())
            .ForMember(destination => destination.Country, source =>
            {
                source.PreCondition(e => e.Country != null);
                source.MapFrom(e => e.Country);
            })
            .ForMember(destination => destination.PostalCode, source =>
            {
                source.PreCondition(e => e.PostalCode != null);
                source.MapFrom(e => e.PostalCode);
            })
            .ForMember(destination => destination.State, source =>
            {
                //source.PreCondition was bitching about ambiguous references. There is virtually no differences between Condition and PreCondition.
                source.Condition(e => e.State != null);
                source.MapFrom(e => e.State);
            })
            .ForMember(destination => destination.StreetAddress, source =>
            {
                source.PreCondition(e => e.StreetAddress != null);
                source.MapFrom(e => e.StreetAddress);
            })
            .ForMember(destination => destination.Suburb, source =>
            {
                source.PreCondition(e => e.Suburb != null);
                source.MapFrom(e => e.Suburb);
            });

        _ = this.CreateMap<UpdateContactRequest, Contact>()
            .ForMember(destination => destination.ContactId, source => source.Ignore())
            .ForMember(destination => destination.Email, source =>
            {
                source.PreCondition(e => e.Email != null);
                source.MapFrom(e => e.Email);
            })
            .ForMember(destination => destination.Mobile, source =>
            {
                source.PreCondition(e => e.Mobile != null);
                source.MapFrom(e => e.Mobile);
            });

    }

    #endregion

}
