using VetCheckup.Domain.Entities;
using VetCheckup.Domain.Enums;

namespace VetCheckup.Application.UseCases.Pets.CreatePet;
public class CreatePetRequest : IRequest
{

    #region Properties
    
    public required DateTime DateOfBirth { get; set; }

    public required string Name { get; set; }

    public required Sex Sex { get; set; }

    public required string Species { get; set; }

    public required string MicrochipId { get; set; }

    public required Guid OwnerId { get; set; }

    #endregion

}
