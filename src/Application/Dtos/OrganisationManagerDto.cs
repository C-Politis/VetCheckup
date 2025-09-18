using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetCheckup.Domain.Entities;
using VetCheckup.Domain.Enums;

namespace VetCheckup.Application.Dtos
{
    public class OrganisationManagerDto
    {
        #region Properties

        public required AddressDto Address { get; set; }

        public required ContactDto ContactDetails { get; set; }

        public required DateTime DateOfBirth { get; set; }

        public required Title Title { get; set; }

        public required string FirstName { get; set; }

        public required string MiddleName { get; set; }

        public required string LastName { get; set; }

        public required Suffix Suffix { get; set; }

        public OrganisationDto? Organisation { get; set; }

        public required Guid OrganisationManagerId { get; set; }

        #endregion
    }
}
