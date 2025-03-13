using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Owners.UpdateOwner
{
    public class UpdateOwnerProfile : Profile
    {

        public UpdateOwnerProfile()
        {
            _ = this.CreateMap<UpdateOwnerRequest, Owner>()
                .ForMember(dest => dest.DateOfBirth, opts =>
                {
                    opts.PreCondition(src => src.DateOfBirth.HasValue);
                    opts.MapFrom(src => src.DateOfBirth);
                })
                .ForMember(dest => dest.Name, opts =>
                {
                    opts.PreCondition(src => src.Name != null);
                    opts.MapFrom(src => src.Name);
                })
                .ForMember(dest => dest.OwnerId, opts => opts.Ignore());

        }

    }
}
