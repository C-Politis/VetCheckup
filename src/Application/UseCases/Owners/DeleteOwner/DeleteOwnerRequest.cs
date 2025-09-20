namespace VetCheckup.Application.UseCases.Owners.DeleteOwner;

public class DeleteOwnerRequest : IRequest
{

    #region Properties

    public Guid OwnerId { get; set; }

    #endregion
}
