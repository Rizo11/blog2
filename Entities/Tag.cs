namespace blog2.Entities;

public class Tag
{
    public Guid Id { get; set; }
    
    public string tag { get; set; }
    
    public Guid postId { get; set; }
    
}