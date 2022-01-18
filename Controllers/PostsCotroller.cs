using Microsoft.AspNetCore.Mvc;
using blog2.ViewModels;
using blog2.Services;
using Microsoft.AspNetCore.Identity;
using blog2.Entities;
using blog2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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
                Tags = p.Tags,
                Accepted = p.Accepted
            })
            .ToListAsync()
        });
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
            CanEdit = _userM.GetUserId(User) == post.CreatedBy.ToString(),
            Accepted = post.Accepted
        };
        return View(model);
    }

    [Authorize(Roles ="admin")]
    [HttpGet("lastposts")]
    public async Task<IActionResult> LastPosts()
    {
        var lastPosts = await _blogDb.BlogsDb.ToListAsync();
        lastPosts = lastPosts.Where(p => (p.CreatedAt > DateTimeOffset.UtcNow.Add(new TimeSpan(-1,0,0,0)) && p.CreatedAt <= DateTimeOffset.UtcNow)).ToList(); 
        lastPosts.AddRange(lastPosts.Where(p => (p.ModifiedAt > DateTimeOffset.UtcNow.Add(new TimeSpan(-1,0,0,0)) && p.ModifiedAt <= DateTimeOffset.UtcNow)).ToList()); 
        
        var model = new PostsViewModel()
        {
            Posts = lastPosts.Select(p => new PostViewModel()
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
                CanEdit = _userM.GetUserId(User) == p.CreatedBy.ToString(),
                Accepted = p.Accepted
            }).ToList()
        };

        return View(model);
    }

}
