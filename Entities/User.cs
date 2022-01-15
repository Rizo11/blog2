using Microsoft.AspNetCore.Identity;

namespace blog2.Entities;

public class User: IdentityUser
{
    public string FullName { get; set; }
    
    public DateTimeOffset Birthdate { get; set; }
    
    public string Name { get; set; }
}