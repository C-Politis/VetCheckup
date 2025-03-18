using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetCheckup.Application.Common.EntityRequests
{
    public class UpdateAddressRequest
    {    
        #region Properties

        public string? Country { get; set; }

        public string? PostalCode { get; set; }

        public string? State { get; set; }

        public string? StreetAddress { get; set; }

        public string? Suburb { get; set; }

        #endregion
    }
}
