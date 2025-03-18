using VetCheckup.Domain.Entities;
using VetCheckup.Domain.Enums;

namespace VetCheckup.Application.Dtos
{
    public class OrganisationDto
    {

        #region Properties

        public required string Abn { get; set; }

        public required Address Address { get; set; }

        public required Contact ContactDetails { get; set; }

        public required string Name { get; set; }

        public Guid OrganisationId { get; set; }

        public required OrganisationType OrganisationType { get; set; }

        #endregion

    }
}
