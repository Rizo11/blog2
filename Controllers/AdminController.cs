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
        _logger.LogWarning($"{user.FullName}");
        return LocalRedirect("/users");
    }
}