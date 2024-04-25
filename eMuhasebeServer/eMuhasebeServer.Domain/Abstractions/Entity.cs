namespace eMuhasebeServer.Domain.Abstractions;
public abstract class Entity
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; } = false;
    protected Entity()
    {
        Id = Guid.NewGuid();
    }
}
