using VetCheckup.Application.Common.Enums;

namespace VetCheckup.Domain.Entities;

public class Owner
{

    #region Properties

    public required Address Address { get; set; }

    public required Contact ContactDetails { get; set; }

    public DateTime DateOfBirth { get; set; }

    public Title? Title { get; set; }
    
    public required string FirstName { get; set; }

    public string? MiddleName { get; set; }

    public required string LastName { get; set; }

    public Suffix? Suffix { get; set; }

    public Guid OwnerId { get; set; }

    
    #endregion

}
