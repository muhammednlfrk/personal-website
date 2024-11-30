namespace PersonalWebsite.Data.Entities;

public class User : EntityBase
{
    public int UserId { get; set; }
    public required string UserFullName { get; set; }
    public required string UserName { get; set; }
    public required string UserPassword { get; set; }
}
