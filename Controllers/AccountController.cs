using System.Text.Json;
using blog2.Entities;
using blog2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace blog2.Controllers;

public class AccountController: Controller
{
    private readonly UserManager<User> _userM;
    private readonly SignInManager<User> _signInManager;
    private readonly ILogger<AccountController> _logger;

    public AccountController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        ILogger<AccountController> logger)
    {
        _userM = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Register(string returnUrl)
    {
        
        return View(new RegisterViewModel(){ ReturnUrl = returnUrl ?? string.Empty});
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        var user = new User()
        {
            FullName = model.Fullname,
            Email = model.Email,
            UserName = model.Username
        };
        var result = await _userM.CreateAsync(user, model.Password);
        if(result.Succeeded)
        {
            return LocalRedirect($"/account/login?returnUrl={model.ReturnUrl}");
        }
        return BadRequest(JsonSerializer.Serialize(result.Errors));
    }
}