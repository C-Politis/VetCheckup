namespace VetCheckup.Application.UseCases.Vets.DeleteVet;

public class DeleteVetRequest : IRequest
{
    
    #region Properties

    public Guid VetId { get; set; }

    #endregion
    
}
