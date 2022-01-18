using blog2.Data;
using blog2.Entities;
using blog2.Services;
using blog2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace blog2.Controllers;

public class AdminController: Controller
{
    private readonly ILogger<AdminController> _logger;
    private readonly BlogDb _blogDb;
    private readonly UserManager<User> _userM;

    public AdminController(BlogDb blog, ILogger<AdminController> logger, UserManager<User> userManager)
    {
        _logger = logger;
        _blogDb = blog;
        _userM = userManager;
    }

    [Authorize(Roles ="admin")]
    [HttpGet("users")]
    public async Task<IActionResult> UserStats()
        => View (await _blogDb.Users.ToListAsync());


    [Authorize(Roles ="admin")]
    [HttpGet("DeleteUser/{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userM.FindByIdAsync(id);
        _blogDb.Users.Remove(user);
        await _userM.DeleteAsync(user);
        _logger.LogInformation($"{user.FullName} was deleted");
        return LocalRedirect("/users");
    }

    [Authorize(Roles="admin")]
    [HttpGet("userposts/{id}")]
    public async Task<IActionResult> UserPosts(string id)
    {
        var user = await _userM.FindByIdAsync(id);
        var result = _blogDb.BlogsDb.Where(p => p.CreatedBy == Guid.Parse(user.Id) );
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

    [Authorize(Roles ="admin")]
    [HttpPost("deny/{id}")]
    public async Task<IActionResult> Deny(Guid id)
    {
        var post = await _blogDb.BlogsDb.FirstOrDefaultAsync(p => p.Id == id);
        post.Accepted = false;
        try
        {
            _blogDb.BlogsDb.Update(post);
            await _blogDb.SaveChangesAsync();
            _logger.LogInformation($"Post {post.Id} was updated succesfully...");
        }
        catch (Exception e)
        {
            _logger.LogError($"Error in updating post, id={post.Id}...{e.Message}");
        }
        return View(post);
    }
}