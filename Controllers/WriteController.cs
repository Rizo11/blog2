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


    [HttpGet("write")]
    public IActionResult Write()
    {
        return View();
    }


    [HttpPost("write")]
    public async Task<IActionResult> Write([FromForm]PostViewModel model)
    {
        var newPost = new Post(model.Title, model.Content);
        var result = await _blogDbService.CreatePostAsync(newPost);
        if (result.IsSuccess)
        {
            return LocalRedirect($"~/post/{newPost.Id}");
        }

        return BadRequest(
            new {
            error = result.Exception.Message,
            status = 400
        }
        );
    }


    // [HttpPost("edit")]
    // public async Task<IActionResult> Edit(Blog blogEntity)
    // {
    //     var blogEntity1 = await _blogDbService.GetBlogByIdAsync(blogEntity.Id);
    //     var result =await _blogDbService.UpdateBlogAsync(blogEntity1);
    //     if (result.IsSuccess)
    //     {
    //         return LocalRedirect($"~/Blogs/{blogEntity.Id}");
    //     }
    //     return BadRequest(result.Exception.Message);
    // }
    // [HttpGet("edit")]
    // public async Task<IActionResult> Edit()
    // {
    //     return View();
    // }

    // [HttpGet("edit/{id}")]
    // public async Task<IActionResult> Edit(Guid id)
    // {
    //     var blogEntity = await _blogDbService.GetBlogByIdAsync(id);
    //     var result = await _blogDbService.DeleteBlogAsync(blogEntity);
    //     if(result.IsSuccess)
    //     {
    //         return View("Write", blogEntity);
    //     }
    //     return BadRequest(
    //         new {
    //         error = result.Exception,
    //         status = 400
    //     });
    // }
}
