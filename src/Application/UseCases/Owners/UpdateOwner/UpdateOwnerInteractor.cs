using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Owners.UpdateOwner
{
    public class UpdateOwnerInteractor(IDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateOwnerRequest>
    {
        #region Methods

        Task IRequestHandler<UpdateOwnerRequest>.Handle(UpdateOwnerRequest request, CancellationToken cancellationToken)
        {
            var owner = dbContext.Get<Owner>().SingleOrDefault(o => o.OwnerId == request.OwnerId) ?? throw new Exception("Owner not found.");

            _ = mapper.Map(request, owner);

            return Task.CompletedTask;
        }

        #endregion
    }
}
