using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Vets.CreateVet;

public class CreateVetInteractor(IDbContext context, IMapper mapper) : IRequestHandler<CreateVetRequest>
{

    #region Methods
    
    Task IRequestHandler<CreateVetRequest>.Handle(CreateVetRequest request, CancellationToken cancellationToken)
    {
        context.Add(mapper.Map<Vet>(request));

        return Task.CompletedTask;
    }

    #endregion

}
