public class Owner : BaseAuditableEntity
{

    public DateTime DateOfBirth { get; set; }
    
    public required string Name { get; set; }
    
    public Guid OwnerId { get; set; }

}
