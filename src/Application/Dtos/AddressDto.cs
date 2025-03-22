using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetCheckup.Application.Dtos
{
    public class AddressDto
    {
       
        #region Properties

        public required Guid AddressId { get; set; }

        public required string Country { get; set; }

        public required string PostalCode { get; set; }

        public required string State { get; set; }

        public required string StreetAddress { get; set; }

        public required string Suburb { get; set; }

        #endregion

    }
}
