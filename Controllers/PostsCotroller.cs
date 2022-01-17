using Microsoft.AspNetCore.Mvc;
using blog2.ViewModels;
using blog2.Services;
using Microsoft.AspNetCore.Identity;
using blog2.Entities;
using blog2.Data;
using Microsoft.EntityFrameworkCore;

namespace blog2.Controllers;

public class PostsController : Controller
{

    private readonly ILogger<PostsController> _logger;
    private readonly BlogDb _blogDb;
    private readonly UserManager<User> _userM;
    public PostsController(ILogger<PostsController> logger, BlogDb blogDb, UserManager<User> userManager)
    {
        _logger = logger;
        _blogDb = blogDb;
        _userM = userManager;
    }


    [HttpGet("posts")]  
    public async Task<IActionResult> GetAllPosts()
    {
        return View(new PostsViewModel()
        {
            Posts = await _blogDb.BlogsDb.Select(p => new PostViewModel()
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
                Tags = p.Tags
            })
            .ToListAsync()
        });


        // var posts =await _blogDbService.GetAllPostsAsync();
        // PostsViewModel ret = await toPostsViewModelAsync(posts);
        // return View(ret);
    }

    [HttpGet("post/{id}")]
    public async Task<IActionResult> Post(Guid Id)
    {
        var post = await _blogDb.BlogsDb.FirstOrDefaultAsync(p => p.Id == Id);
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
            Author = post.CreatedBy.ToString(),
            Tags = post.Tags,
            CanEdit = _userM.GetUserId(User) == post.CreatedBy.ToString()
        };
        return View(model);
    }

}
