using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Pets.UpdatePet
{

    public class UpdatePetProfile : Profile
    {
        
        #region Constructors

        public UpdatePetProfile()
            => CreateMap<UpdatePetRequest, Pet>()
                .ForMember(dest => dest.Name, opts =>
                {
                    opts.PreCondition(src => src.Name != null);
                    opts.MapFrom(src => src.Name);
                })
                .ForMember(dest => dest.Species, opts =>
                {
                    opts.PreCondition(src => src.Species != null);
                    opts.MapFrom(src => src.Species);
                })
                .ForMember(dest => dest.Sex, opts =>
                {
                    opts.PreCondition(src => src.Sex != null);
                    opts.MapFrom(src => src.Sex);
                })
                .ForMember(dest => dest.MicrochipId, opts =>
                {
                    opts.PreCondition(src => src.MicrochipId != null);
                    opts.MapFrom(src => src.MicrochipId);
                })
                .ForMember(dest => dest.Owner, opts => opts.Ignore())
                .ForMember(dest => dest.PetId, opts => opts.Ignore());

        #endregion

    }

}
