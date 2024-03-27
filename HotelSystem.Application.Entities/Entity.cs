namespace HotelSystem.Application.Entities;

public abstract class Entity
{
    public Guid Id { get;  set; }
    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }
    protected Entity(Guid id)
    {
        Id = id;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    protected Entity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
