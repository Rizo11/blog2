using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using blog2.Entities;

namespace blog2.Data;

public class BlogDb: IdentityDbContext<User>
{
    public DbSet<Post> BlogsDb { get; set; }
    public BlogDb( DbContextOptions<BlogDb> options)
        :base(options){ }
 
}