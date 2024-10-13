using VetCheckup.Application.Common.EntityRequests;

namespace VetCheckup.Application.UseCases.Owners.CreateOwner;

public class CreateOwnerRequest : IRequest
{
    #region Properties

    public required CreateAddressRequest Address { get; set; }

    public required CreateContactRequest ContactDetails { get; set; }

    public required DateTime DateOfBirth { get; set; }

    public required string Name { get; set; }

    #endregion

}
