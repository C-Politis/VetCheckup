using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.Dtos
{
    public class OwnerDto
    {

        #region Properties

        public required AddressDto Address { get; set; }

        public required ContactDto ContactDetails { get; set; }

        public DateTime DateOfBirth { get; set; }

        public required string Name { get; set; }

        public Guid OwnerId { get; set; }

        #endregion

    }
}
