using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetCheckup.Application.Common.Enums;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.Dtos
{
    public class OwnerDto
    {

        #region Properties

        public required AddressDto Address { get; set; }

        public required ContactDto ContactDetails { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Title? Title { get; set; }
        
        public required string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public required string LastName { get; set; }

        public Suffix? Suffix { get; set; }

        public Guid OwnerId { get; set; }

        #endregion

    }
}
