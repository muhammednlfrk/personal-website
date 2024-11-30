namespace PersonalWebsite.Data.Entities;

public sealed class Post : EntityBase
{
    public required string PostId { get; set; }
    public required string PostTitle { get; set; }
    public required string PostDescription { get; set; }
}
