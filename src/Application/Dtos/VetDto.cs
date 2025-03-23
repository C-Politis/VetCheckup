using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.Dtos
{
    public class VetDto
    {

        #region Properties

        public required Address Address { get; set; }

        public required Contact ContactDetails { get; set; }

        public DateTime DateOfBirth { get; set; }

        public required string Name { get; set; }

        public Guid VetId { get; set; }
        
        public required ICollection<VetOrganisation> VetOrganisations { get; set; }

        #endregion

    }
}
