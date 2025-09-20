using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Owners.DeleteOwner;

public class DeleteOwnerInteractor(IDbContext context) : IRequestHandler<DeleteOwnerRequest>
{

    #region Methods

    Task IRequestHandler<DeleteOwnerRequest>.Handle(DeleteOwnerRequest request, CancellationToken cancellationToken)
    {
        var owner = context.Get<Owner>().FirstOrDefault(e => e.OwnerId == request.OwnerId) ?? throw new Exception("Owner not found");
        context.Remove(owner);
        return Task.CompletedTask;
    }

    #endregion
}
