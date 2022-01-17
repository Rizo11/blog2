using blog2.Entities;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace blog2;
public class SeedDb: BackgroundService
{
    private readonly ILogger<SeedDb> _logger;
    private readonly IServiceProvider _serviceProvider;
    private UserManager<User> _userManager;
    private RoleManager<IdentityRole> _roleManager;
    private readonly InitialData _initialData;
    public SeedDb(
        ILogger<SeedDb> logger,
        IServiceProvider serviceProvider,
        IConfiguration config
    )
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _initialData = config.GetSection("InitialData").Get<InitialData>();
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateAsyncScope();
        _userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        if(_initialData.SeedDb)
        {
            _logger.LogInformation("Starting SEED process...");

            if(_initialData.Roles.Count() > 0)
            {
                foreach(var role in _initialData.Roles)
                {
                    if(!await _roleManager.RoleExistsAsync(role.ToLower()))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(role.ToLower()));
                        
                        _logger.LogInformation($"{role.ToUpper()} role is created.");
                    }
                }
            }

            if(_initialData.Users.Count() > 0)
            {
                foreach(var user in _initialData.Users)
                {
                    var realUser = await _userManager.FindByEmailAsync(user.Email);
                    if(realUser is null)
                    {
                        realUser = new User()
                        {
                            Email = user.Email,
                            FullName = user.Fullname,
                            UserName = user.Username
                        };

                        var result = await _userManager.CreateAsync(realUser, user.Password);
                        if(result.Succeeded)
                        {
                            _logger.LogInformation($"User with email {user.Email.ToUpper()} is created.");
                        }
                        else
                        {
                            _logger.LogError($"{JsonSerializer.Serialize(result.Errors)}");
                        }
                        
                        if(user.Roles.Count() > 0)
                        {
                            foreach(var role in user.Roles)
                            {
                                if(await _roleManager.RoleExistsAsync(role.ToLower()))
                                {
                                    await _userManager.AddToRoleAsync(realUser, role.ToLower());
                                    _logger.LogInformation($"{realUser.Email} is added to role {role.ToUpper()}");
                                }
                            }
                        }
                    }

                }
            }
        }
    }
}
    public class InitialData
    {
        public bool SeedDb { get; set; }
        
        public IEnumerable<string> Roles { get; set; }
        
        public IEnumerable<SeedUser> Users { get; set; }
    }

    public class SeedUser
    {
        public string Fullname { get; set; }
        
        public string Email { get; set; }

        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public IEnumerable<string> Roles { get; set; }
    }