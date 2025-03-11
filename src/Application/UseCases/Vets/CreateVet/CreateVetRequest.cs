using VetCheckup.Application.Common.EntityRequests;

namespace VetCheckup.Application.UseCases.Vets.CreateVet;

public class CreateVetRequest : IRequest
{
    public required CreateAddressRequest Address { get; set; }
    
    public required CreateContactRequest ContactDetails { get; set; }
    
    public required DateTime DateOfBirth { get; set; }
    
    public required string Name { get; set; }
}
