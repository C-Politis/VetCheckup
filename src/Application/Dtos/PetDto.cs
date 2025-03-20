using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetCheckup.Domain.Enums;

namespace VetCheckup.Application.Dtos
{
    public class PetDto
    {

        #region Properties

        public required OwnerDto Owner { get; set; }

        public required DateTime DateOfBirth { get; set; }

        public required string Name { get; set; }

        public required Guid PetId { get; set; }

        public required Sex Sex { get; set; }

        public required string Species { get; set; }

        public required string MicrochipId { get; set; }

        #endregion

    }
}
