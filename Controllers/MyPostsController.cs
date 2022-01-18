using blog2.Data;
using blog2.Entities;
using blog2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace blog2.Controllers;

public class MyPostsController: Controller
{
    private readonly ILogger<MyPostsController> _logger;
    private readonly BlogDb _blogDb;
    private readonly UserManager<User> _userM;

    public MyPostsController(BlogDb blogDb, ILogger<MyPostsController> logger, UserManager<User> userManager)
    {
        _logger = logger;
        _blogDb = blogDb;
        _userM = userManager;
    }
    [Authorize]
    [HttpGet("myposts")]
    public IActionResult MyPosts()
    {
        var userId = _userM.GetUserId(User);
        var result = _blogDb.BlogsDb.Where(p => p.CreatedBy == Guid.Parse(userId));
        var posts = new PostsViewModel(){
            Posts = result.Select(p => new PostViewModel()  
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                Edited = p.Edited,
                Likes = p.Likes,
                Dislikes = p.Dislikes,
                CreatedAt = p.CreatedAt,
                ModifiedAt = p.ModifiedAt,
                Author = p.CreatedBy.ToString(),
                Tags = p.Tags,
                Accepted = p.Accepted
            }).ToList()
        };

        return View(posts);
    }
}