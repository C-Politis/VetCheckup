using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetCheckup.Application.Dtos;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Owners.GetOwner
{
    public class GetOwnerInteractor(IDbContext dbContext, IMapper mapper) : IRequestHandler<GetOwnerRequest, OwnerDto>
    {

        #region Methods

        Task<OwnerDto> IRequestHandler<GetOwnerRequest, OwnerDto>.Handle(GetOwnerRequest request, CancellationToken cancellationToken)
        {
            var owner = dbContext.Get<Owner>().SingleOrDefault(o => o.OwnerId == request.OwnerID) ?? throw new Exception($"Owner not found.");

            return Task.FromResult(mapper.Map<OwnerDto>(owner));
        }

        #endregion
    }
}
