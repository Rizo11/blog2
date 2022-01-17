using System;
using System.ComponentModel.DataAnnotations;
using blog2.Entities;

namespace blog2.ViewModels;
public class PostViewModel
{
    public Guid? Id { get; set; } = default(Guid);
    // public Guid Id { get; set; }
    
    public string Title { get; set; }
    public string Content { get; set; }

    public bool Edited { get; set; }

    public ulong Likes { get; set; }
    public ulong Dislikes { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
    public string Tags { get; set; }
    public string Author { get; set; }
    public bool CanEdit { get; set; }
    
    
}



    // public List<string> Tags { get; set; }
    // public List<string> Comments { get; set; }

    //when problem occures change the Write.cshtml hidden input
    //for likes
    //for disliked
    //