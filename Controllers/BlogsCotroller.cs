using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using blog2.Models;
using blog2.ViewModels;
using blog2.Services;

namespace blog2.Controllers;

public class BlogsController : Controller
{

    private readonly ILogger<BlogsController> _logger;
    private readonly IBlogDbService _blogDbService;
    public BlogsController(IBlogDbService blogDbService, ILogger<BlogsController> logger)
    {
        _blogDbService = blogDbService;
        _logger = logger;
    }


    [HttpGet("Blogs")]
    public async Task<IActionResult> GetAllBlogs()
    {
        var blogs = await _blogDbService.GetAllBlogsAsync();
        return View(blogs);
    }

    [HttpGet("Blogs/{id}")]
    public async Task<IActionResult> Blogs(Guid Id)
    {
        var blogEntity = await _blogDbService.GetBlogByIdAsync(Id);
        return View(blogEntity);
    }

}
