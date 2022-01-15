namespace blog2.Entities;

public class Comment
{           
    public Guid Id { get; set; }
    
    public string commentBody { get; set; }
    
    public Guid PostId { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset ModifiedAt { get; set; }
    
    public Guid CommentorId { get; set; }   
    
}