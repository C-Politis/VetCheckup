using VetCheckup.Application.Dtos;

namespace VetCheckup.Application.UseCases.Pets.GetPet
{
    public class GetPetRequest : IRequest<PetDto>
    {

        #region Properties
        
        public required Guid PetId { get; set; }
        
        #endregion

    }
}
