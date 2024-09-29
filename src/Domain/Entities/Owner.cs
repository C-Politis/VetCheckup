public class Owner : BaseAuditableEntity
{
    public Guid OwnerId { get; set; }

    public required string Name { get; set; }

    public DateTime DateOfBirth { get; set; }
}
