using VetCheckup.Application.Common.EntityRequests;

namespace VetCheckup.Application.UseCases.Vets.UpdateVet;

public class UpdateVetRequest : IRequest
{

    #region Properties
    public required Guid VetId { get; set; }

    public required UpdateAddressRequest Address { get; set; }
    
    public required UpdateContactRequest ContactDetails { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Name { get; set; }

    #endregion

}
