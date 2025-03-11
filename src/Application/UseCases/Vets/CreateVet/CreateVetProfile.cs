using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Vets.CreateVet;

public class CreateVetProfile : Profile
{

    #region Constructors

    public CreateVetProfile()
    {
        _ = CreateMap<CreateVetRequest, Vet>()
            .ForMember(destination => destination.VetId, source => source.Ignore());
    }

    #endregion
}
