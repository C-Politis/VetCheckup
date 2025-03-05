using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetCheckup.Application.Dtos;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Pets.GetPet
{
    public class GetPetInteractor(IDbContext dbContext, IMapper mapper) : IRequestHandler<GetPetRequest, PetDto>
    {
        #region Methods
        
        Task<PetDto> IRequestHandler<GetPetRequest, PetDto>.Handle(GetPetRequest request, CancellationToken cancellationToken)
        {
            var pet = dbContext.Get<Pet>().SingleOrDefault(p => p.PetId == request.PetId) ?? throw new Exception($"Pet not found.");

            return Task.FromResult(mapper.Map<PetDto>(pet));
        } 

        #endregion
    }
}
