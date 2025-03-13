using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetCheckup.Application.Common.EntityRequests
{
    public class UpdateContactRequest
    {
        
        #region Properties

        public string? Email { get; set; }

        public int? Mobile { get; set; }

        #endregion

    }
}
