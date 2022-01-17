using Microsoft.AspNetCore.Mvc;
using blog2.ViewModels;
using blog2.Services;
using blog2.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace blog2.Controllers;

public class WriteController : Controller
{
    private readonly ILogger<WriteController> _logger;
    private readonly IBlogDbService _blogDbService;
    private readonly UserManager<User> _userM;
    public WriteController(IBlogDbService blogDbService, ILogger<WriteController> logger, UserManager<User> userManager)
    {
        _blogDbService = blogDbService;
        _logger = logger;
        _userM = userManager;
    }

    [Authorize]
    [HttpGet("write")]
    public IActionResult Write()
    {
        return View();
    }

    [Authorize]
    [HttpPost("write")]
    public async Task<IActionResult> Write([FromForm]PostViewModel model)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest($"{ModelState.ErrorCount} errors detected");
        }

        if(model.Edited)
        {
            //here is the mistake
            //GetPostByIdAsync(system.guid)
            // but model has system.guid?
            //so i changed the model
            Guid s = model.Id != null? Guid.Parse(model.Id.ToString()): default(Guid);
            var post = await _blogDbService.GetPostByIdAsync(s);
            if(post.Title == model.Title && post.Content == model.Content && post.Tags == model.Tags)
            {
                return LocalRedirect($"~/post/{post.Id}");
            }
            post.ModifiedAt = DateTimeOffset.UtcNow;
            post.Title = model.Title;
            post.Content = model.Content;
            post.Tags = model.Tags;

            var res = await _blogDbService.UpdatePostAsync(post);
            if(res.IsSuccess)
            {
                return LocalRedirect($"~/post/{post.Id}");
            }
            return BadRequest(
                new{
                    error = res.Exception.Message,
                    status = 401
            });
        }
        
        var userId = _userM.GetUserId(User);
        var newPost = new Post(model.Title, model.Content, Guid.Parse(userId), model.Tags);
        var result = await _blogDbService.CreatePostAsync(newPost);
        if (result.IsSuccess)
        {
            return LocalRedirect($"~/post/{newPost.Id}");
        }

        return BadRequest(
            new {
            error = result.Exception.Message,
            status = 400
        });
    }


    [HttpGet("edit/{id}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var post = await _blogDbService.GetPostByIdAsync(id);
        var model = new PostViewModel()
        {
            Id = post.Id,
            Edited = true,
            Likes = post.Likes,
            Dislikes = post.Dislikes,
            Author = "author",
            Tags = post.Tags,
            
            Title = post.Title,
            Content = post.Content,

            CreatedAt = post.CreatedAt,
            ModifiedAt = post.ModifiedAt,       

        };

        return View("Write", model);
    }

    [HttpGet("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _blogDbService.DeletePostAsync(id);
        if(!result.IsSuccess)
        {
            return BadRequest(
                new {
                error = result.Exception.Message,
                status = 400
        });
        }
        var userId = _userM.GetUserId(User);
        var usersPosts = _blogDbService.GetUserPosts(userId);
        var posts = new PostsViewModel(){
            Posts = usersPosts.Select(p => new PostViewModel()  
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
            }).ToList()
        };

        return LocalRedirect("/myposts");
    }
}
