using VetCheckup.Application.Dtos;

namespace VetCheckup.Application.UseCases.Owners.GetOwner
{
    public class GetOwnerRequest : IRequest<OwnerDto>
    {

        #region Properties

        public required Guid OwnerID { get; set; }

        #endregion

    }
}
