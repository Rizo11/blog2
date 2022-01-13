using System.Reflection.Metadata;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using blog2.Models;
using blog2.ViewModels;
using blog2.Services;
using blog2.Entities;

namespace blog2.Controllers;

public class WriteController : Controller
{
    private readonly ILogger<WriteController> _logger;
    private readonly IBlogDbService _blogDbService;
    public WriteController(IBlogDbService blogDbService, ILogger<WriteController> logger)
    {
        _blogDbService = blogDbService;
        _logger = logger;
    }


    [HttpGet("Write")]
    public IActionResult Write()
    {
        return View();
    }


    [HttpPost("Write")]
    public async Task<IActionResult> Write([FromForm]Blog blog)
    {
        if(await _blogDbService.ExistsBlogAsync(blog.Id))
        {
            var res =await _blogDbService.UpdateBlogAsync(blog);
            if (res.IsSuccess)
            {
                return LocalRedirect($"~/Blogs/{blog.Id}");
            }
            return BadRequest(res.Exception.Message);
        }
        blog.Id = Guid.NewGuid();
        blog.CreatedAt = DateTimeOffset.UtcNow;
        blog.ModifiedAt = blog.CreatedAt;
        blog.Tags = "a";
        blog.Comments = "b";
        blog.Likes = 1;
        blog.Dislikes = 2;
        var result = await _blogDbService.PostBlogAsync(blog);
        if (result.IsSuccess)
        {
            return LocalRedirect($"~/Blogs/{blog.Id}");
        }

        return BadRequest(result.Exception.Message);
    }


    [HttpPost("Edit")]
    public async Task<IActionResult> Edit(Blog blogEntity)
    {
        var result =await _blogDbService.UpdateBlogAsync(blogEntity);
        if (result.IsSuccess)
        {
            return LocalRedirect($"~/Blogs/{blogEntity.Id}");
        }
        return BadRequest(result.Exception.Message);
    }

    [HttpGet("edit/{id}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var blogEntity = await _blogDbService.GetBlogByIdAsync(id);
        blogEntity.ModifiedAt = DateTimeOffset.UtcNow;
        return View("Write", blogEntity);
    }
}
