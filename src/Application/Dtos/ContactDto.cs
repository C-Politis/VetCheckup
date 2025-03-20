using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetCheckup.Application.Dtos
{
    public class ContactDto
    {

        #region Properties

        public required Guid ContactId { get; set; }

        public required string Email { get; set; }

        public required string Mobile { get; set; }

        #endregion

    }
}
