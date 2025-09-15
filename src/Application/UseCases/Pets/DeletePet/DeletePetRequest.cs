namespace VetCheckup.Application.UseCases.Pets.DeletePet;

public class DeletePetRequest : IRequest
{
    
    #region Properties

    public Guid PetId { get; set; }

    #endregion
    
}
