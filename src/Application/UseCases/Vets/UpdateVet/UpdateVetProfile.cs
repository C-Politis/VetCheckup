using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Vets.UpdateVet;

public class UpdateVetProfile : Profile
{

    #region Constructors

    public UpdateVetProfile()
    {
        _ = this.CreateMap<UpdateVetRequest, Vet>()
            .ForMember(dest => dest.Name, opts =>
            {
                opts.PreCondition(src => src.Name != null);
                opts.MapFrom(src => src.Name);
            })
            .ForMember(dest => dest.DateOfBirth, opts =>
            {
                opts.PreCondition(src => src.DateOfBirth.HasValue);
                opts.MapFrom(src => src.DateOfBirth);
            })
            .ForMember(dest => dest.VetId, opts => opts.Ignore())
            .ForMember(dest => dest.VetOrganisations, opts => opts.Ignore());
    }

    #endregion

}
