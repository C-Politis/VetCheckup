using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Owners.CreateOwner;

public class CreateOwnerInteractor(IDbContext context, IMapper mapper) : IRequestHandler<CreateOwnerRequest>
{

    #region Methods

    Task IRequestHandler<CreateOwnerRequest>.Handle(CreateOwnerRequest request, CancellationToken cancellationToken)
    {
        context.Add(mapper.Map<Owner>(request));

        return Task.CompletedTask;
    }

    #endregion

}
