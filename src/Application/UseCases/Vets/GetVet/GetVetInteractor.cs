using VetCheckup.Application.Dtos;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Vets.GetVet
{
    public class GetVetInteractor(IApplicationDbContext applicationDbContext, IMapper mapper) : IRequestHandler<GetVetRequest, VetDto>
    {

        #region Methods

        Task<VetDto> IRequestHandler<GetVetRequest, VetDto>.Handle(GetVetRequest request, CancellationToken cancellationToken)
        {
            var vet = applicationDbContext.Get<Vet>().SingleOrDefault(v => v.VetId == request.VetId) ?? throw new Exception("Vet not found.");

            return Task.FromResult(mapper.Map<VetDto>(vet));
        }

        #endregion

    }
}
