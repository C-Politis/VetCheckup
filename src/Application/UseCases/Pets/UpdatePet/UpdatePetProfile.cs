using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Pets.UpdatePet
{

    public class UpdatePetProfile : Profile
    {
        #region Constructors

        public UpdatePetProfile()
            => CreateMap<UpdatePetRequest, Pet>()
                        .ForMember(destination => destination.Owner, source => source.Ignore());

        #endregion

    }

}
