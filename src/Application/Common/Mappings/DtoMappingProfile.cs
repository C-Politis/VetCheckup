using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetCheckup.Application.Dtos;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.Common.Mappings
{
    public class DtoMappingProfile : Profile
    {

        #region Constructors

        public DtoMappingProfile()
        {
            _ = this.CreateMap<Address, AddressDto>();

            _ = this.CreateMap<Contact, ContactDto>();

            _ = this.CreateMap<Owner, OwnerDto>();

            _ = this.CreateMap<Pet, PetDto>();
        } 

        #endregion

    }
}
