using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Vets.UpdateVet;

public class UpdateVetInteractor(IDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateVetRequest>
{

    #region Methods

    Task IRequestHandler<UpdateVetRequest>.Handle(UpdateVetRequest request, CancellationToken cancellationToken)
    {
        var vet = dbContext.Get<Vet>().SingleOrDefault(v => v.VetId == request.VetId);

        _ = mapper.Map(request, vet);

        return Task.CompletedTask;
    }

    #endregion

}
