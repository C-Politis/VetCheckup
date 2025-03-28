﻿using VetCheckup.Domain.Enums;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.Dtos
{
    public class VetDto
    {

        #region Properties

        public required Address Address { get; set; }

        public required Contact ContactDetails { get; set; }

        public DateTime DateOfBirth { get; set; }

        public required Title Title { get; set; }

        public required string FirstName { get; set; }

        public required string MiddleName { get; set; }

        public required string LastName { get; set; }

        public required Suffix Suffix { get; set; }

        public Guid VetId { get; set; }
        
        public required ICollection<VetOrganisation> VetOrganisations { get; set; }

        #endregion

    }
}
