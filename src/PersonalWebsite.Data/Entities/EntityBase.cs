namespace PersonalWebsite.Data.Entities;

public abstract class EntityBase
{
    public virtual DateTime CreationTime { get; set; }
    public virtual DateTime? UpdateTime { get; set; }
}
