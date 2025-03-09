using System;
using System.Collections.Generic;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Pets.UpdatePet
{

    public class UpdatePetInteractor(IDbContext context, IMapper mapper) : IRequestHandler<UpdatePetRequest>
    {
        #region Methods

        Task IRequestHandler<UpdatePetRequest>.Handle(UpdatePetRequest request, CancellationToken cancellationToken)
        {
            var pet = context.Get<Pet>().FirstOrDefault(e => e.PetId == request.PetId) ?? throw new Exception("Pet not found");
            if (request.OwnerId != Guid.Empty)
            {
                var owner = context.Get<Owner>().FirstOrDefault(e => e.OwnerId == request.OwnerId) ?? throw new Exception("Owner not found");
                pet.Owner = owner;
            }

            mapper.Map(request, pet);

            return Task.CompletedTask;
        }

        #endregion

    }

}
