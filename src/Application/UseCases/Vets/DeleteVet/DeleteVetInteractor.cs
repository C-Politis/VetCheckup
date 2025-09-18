using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Vets.DeleteVet;

public class DeleteVetInteractor(IDbContext context) : IRequestHandler<DeleteVetRequest>
{
    
    #region Methods

    Task IRequestHandler<DeleteVetRequest>.Handle(DeleteVetRequest request, CancellationToken cancellationToken)
    {
        var vet = context.Get<Vet>().FirstOrDefault(e => e.VetId == request.VetId) ?? throw new Exception($"Vet with id {request.VetId} not found.");

        context.Remove(vet);
        return Task.CompletedTask;
    }

    #endregion

}
