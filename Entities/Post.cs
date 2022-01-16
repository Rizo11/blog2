using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blog2.Entities;

public class Post
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    
    public string Content { get; set; }

    public Guid CreatedBy { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset ModifiedAt { get; set; }
    public ulong Likes { get; set; }
    
    public ulong Dislikes { get; set; }

    public bool Edited => CreatedAt != ModifiedAt;


    [Obsolete("Not allowed", true)]
    public Post(){}

    public Post(string title, string content, Guid createdBy)
    {
        Title = title;
        Content = content;
        CreatedBy = createdBy;

        Id = Guid.NewGuid();
        CreatedAt = ModifiedAt = DateTimeOffset.UtcNow;
        Likes = 0;
        Dislikes = 0;
    }
}