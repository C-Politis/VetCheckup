using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetCheckup.Application.Dtos;

namespace VetCheckup.Application.UseCases.Owners.GetOwner
{
    public class GetOwnerRequest : IRequest
    {

        #region Properties

        public required Guid OwnerID { get; set; }

        #endregion

    }
}
