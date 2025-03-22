namespace VetCheckup.Application.Common.EntityRequests;

public class CreateContactRequest
{

    #region Properties

    public required string Email { get; set; }

    public required string Mobile { get; set; }

    #endregion

}
