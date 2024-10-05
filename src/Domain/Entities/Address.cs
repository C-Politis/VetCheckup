namespace VetCheckup.Domain.Entities;

public class Address
{

    #region Properties

    public Guid AddressId { get; set; }

    public required string Country { get; set; }

    public required string PostalCode { get; set; }

    public required string State { get; set; }

    public required string StreetAddress { get; set; }

    public required string Suburb { get; set; }

    #endregion

}
