using System.Text;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using blog2.Models;
using blog2.ViewModels;
using blog2.Services;

namespace blog2.Controllers;

public class PostsController : Controller
{

    private readonly ILogger<PostsController> _logger;
    private readonly IBlogDbService _blogDbService;
    public PostsController(IBlogDbService blogDbService, ILogger<PostsController> logger)
    {
        _blogDbService = blogDbService;
        _logger = logger;
    }


    [HttpGet("posts")]  
    public async Task<IActionResult> GetAllPosts()
    {
        var posts = await _blogDbService.GetAllPostsAsync();
        return View(posts);
    }

    [HttpGet("post/{id}")]
    public async Task<IActionResult> Post(Guid Id)
    {
        var post = await _blogDbService.GetPostByIdAsync(Id);
        var model = new PostViewModel()
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            Edited = post.Edited,
            Likes = post.Likes,
            Dislikes = post.Dislikes,
            CreatedAt = post.CreatedAt,
            ModifiedAt = post.ModifiedAt,
            // Author = "name of the Author",
            // Tags = post.Comments.ToString().Split(',').ToList(),
            // Comments = post.Comments.ToString().Split(',').ToList()
        };
        return View(model);
    }

}
