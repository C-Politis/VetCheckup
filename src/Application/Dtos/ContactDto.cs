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

        public Guid ContactId { get; set; }

        public string? Email { get; set; }

        public int? Mobile { get; set; }

        #endregion

    }
}
