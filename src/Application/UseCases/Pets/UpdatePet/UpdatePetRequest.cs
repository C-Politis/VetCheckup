using VetCheckup.Domain.Enums;

namespace VetCheckup.Application.UseCases.Pets.UpdatePet;

public class UpdatePetRequest : IRequest
{

    #region Properties
    
    public required Guid PetId { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
    public required string Name { get; set; }

    public Sex Sex { get; set; }

    public required string Species { get; set; }

    public int? MicrochipId { get; set; }

    public required Guid OwnerId { get; set; }

    #endregion

}
