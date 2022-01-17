using System.Net;
using blog2.Data;
using blog2.Entities;
using blog2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace blog2.Controllers;

public class SearchController: Controller
{
    private readonly ILogger<SearchController> _logger;
    private readonly BlogDb _blogDb;
    private readonly UserManager<User> _userM;

    public SearchController(BlogDb blogDb, ILogger<SearchController> logger, UserManager<User> userManager)
    {
        _logger = logger;
        _blogDb = blogDb;
        _userM = userManager;
    }
    [HttpGet("{id}")]
    public IActionResult byTag(string id)
    {
        var result = _blogDb.BlogsDb.Where(x => x.Tags.Contains(id)).ToList();
        var p = new PostsViewModel(){
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
                Tags = p.Tags
            }).ToList()
        };
        return View(p);
    }

    [HttpPost("{query}")]   
    public IActionResult byTagTitle(string query)
    {
        var result = _blogDb.BlogsDb.Where(x => x.Tags.Contains(query)).ToList();
        var p = new PostsViewModel(){
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
                Tags = p.Tags
            }).ToList()
        };
        result = _blogDb.BlogsDb.Where(x => x.Title.Contains(query)).ToList();
        p.Posts.AddRange(
            result.Select(p => new PostViewModel()  
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
        );
        return View(p);
    }


}