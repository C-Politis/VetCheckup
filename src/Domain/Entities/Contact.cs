namespace VetCheckup.Domain.Entities;

public class Contact
{

    #region Properties

    public Guid ContactId { get; set; }

    public required string Email { get; set; }

    public required string Mobile { get; set; }

    #endregion

}
