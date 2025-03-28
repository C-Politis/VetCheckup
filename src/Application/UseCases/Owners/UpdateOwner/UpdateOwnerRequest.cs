﻿using VetCheckup.Application.Common.EntityRequests;
using VetCheckup.Domain.Enums;

namespace VetCheckup.Application.UseCases.Owners.UpdateOwner
{
    public class UpdateOwnerRequest : IRequest
    {

        #region Properties

        public required Guid OwnerId { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public Title? Title { get; set; }

        public Suffix? Suffix { get; set; }

        public required UpdateAddressRequest Address { get; set; }

        public required UpdateContactRequest ContactDetails { get; set; }

        #endregion

    }
}
