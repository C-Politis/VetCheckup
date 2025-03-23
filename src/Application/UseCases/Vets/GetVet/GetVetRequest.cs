using VetCheckup.Application.Dtos;

namespace VetCheckup.Application.UseCases.Vets.GetVet
{
    public class GetVetRequest : IRequest<VetDto>
    {

        #region Properties

        public required Guid VetId { get; set; }

        #endregion

    }
}

