using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Pets.DeletePet;

public class DeletePetInteractor(IApplicationDbContext context) : IRequestHandler<DeletePetRequest>
{
    #region Methods

    Task IRequestHandler<DeletePetRequest>.Handle(DeletePetRequest request, CancellationToken cancellationToken)
    {
        var pet = context.Get<Pet>().FirstOrDefault(e => e.PetId == request.PetId) ?? throw new Exception("Pet not found");
        context.Remove(pet);
        return Task.CompletedTask;
    }

    #endregion
}
