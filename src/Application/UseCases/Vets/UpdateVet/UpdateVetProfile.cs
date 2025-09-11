using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Vets.UpdateVet;

public class UpdateVetProfile : Profile
{

    #region Constructors

    public UpdateVetProfile()
    {
        _ = this.CreateMap<UpdateVetRequest, Vet>()
            .ForMember(dest => dest.FirstName, opts =>
            {
                opts.PreCondition(src => src.FirstName != null);
                opts.MapFrom(src => src.FirstName);
            })
            .ForMember(dest => dest.MiddleName, opts =>
            {
                opts.PreCondition(src => src.MiddleName != null);
                opts.MapFrom(src => src.MiddleName);
            })
            .ForMember(dest => dest.LastName, opts =>
            {
                opts.PreCondition(src => src.LastName != null);
                opts.MapFrom(src => src.LastName);
            })
            .ForMember(dest => dest.Title, opts =>
            {
                opts.PreCondition(src => src.Title != null);
                opts.MapFrom(src => src.Title);
            })
            .ForMember(dest => dest.Suffix, opts =>
            {
                opts.PreCondition(src => src.Suffix != null);
                opts.MapFrom(src => src.Suffix);
            })
            .ForMember(dest => dest.DateOfBirth, opts =>
            {
                opts.PreCondition(src => src.DateOfBirth.HasValue);
                opts.MapFrom(src => src.DateOfBirth);
            })
            .ForMember(dest => dest.VetId, opts => opts.Ignore())
            .ForMember(dest => dest.VetOrganisations, opts => opts.Ignore())
            .ForMember(dest => dest.User, opts => opts.Ignore());
    }

    #endregion

}
