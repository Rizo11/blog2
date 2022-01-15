using System;
using System.ComponentModel.DataAnnotations;

namespace blog2.ViewModels;
public class PostViewModel
{
    public Guid? Id { get; set; } = default(Guid);
    
    [Required]
    [MaxLength(255)]
    public string Title { get; set; }
    
    [Required]
    public string Content { get; set; }

    public bool Edited { get; set; }

    public ulong Likes { get; set; }
    public ulong Dislikes { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
    // public string Author { get; set; }
    // public List<string> Tags { get; set; }
    // public List<string> Comments { get; set; }
}