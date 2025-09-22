using VetCheckup.Application.Services.Persistence;

namespace VetCheckup.Application.UseCases.Pets.CreatePet;
public class CreatePetInteractor(IApplicationDbContext context, IMapper mapper) : IRequestHandler<CreatePetRequest>
{

    #region Methods

    Task IRequestHandler<CreatePetRequest>.Handle(CreatePetRequest request, CancellationToken cancellationToken)
    {
        var owner = context.Get<Domain.Entities.Owner>().FirstOrDefault(e => e.OwnerId == request.OwnerId) ?? throw new Exception("Owner not found");

        var pet = mapper.Map<Domain.Entities.Pet>(request);
        pet.Owner = owner;

        context.Add(pet);

        return Task.CompletedTask;
    }

    #endregion

}

