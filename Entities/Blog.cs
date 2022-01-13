using System.Net.Mime;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blog2.Entities;

public class Blog
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(256)]
    public string Title { get; set; }
    
    [Required]
    public string Content { get; set; }
    
    [Required]
    public DateTimeOffset CreatedAt { get; set; }
    
    [Required]
    public DateTimeOffset ModifiedAt { get; set; }

    public string Tags { get; set; }
    
    public int Likes { get; set; }
    
    public int Dislikes { get; set; }

    public string Comments { get; set; }


    // public Blog(string title, string content, DateTimeOffset createdAt, DateTimeOffset modifiedAt, string tags, int likes, int dislikes, string comments)
    // {
    //     Id = Guid.NewGuid();
    //     Title = title;
    //     Content = content;
    //     CreatedAt = createdAt;
    //     ModifiedAt = modifiedAt;
    //     Tags = tags;
    //     Likes = likes;
    //     Dislikes = dislikes;
    //     Comments = comments;
    // }
}