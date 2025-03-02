using VetCheckup.Domain.Entities;
namespace VetCheckup.Application.UseCases.Pets.CreatePet;

public class CreatePetProfile : Profile
{

    #region Constructors

    public CreatePetProfile()
    {
        _ = CreateMap<CreatePetRequest, Pet>()
            .ForMember(destination => destination.PetId, source => source.Ignore())
            .ForMember(destination => destination.Owner, source => source.Ignore());
    }

    #endregion

}
