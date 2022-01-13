using Microsoft.EntityFrameworkCore;
using blog2.Entities;

namespace mvc6.Data;

public class BlogDb: DbContext
{
    public DbSet<Blog> BlogsDb { get; set; }
    public BlogDb( DbContextOptions options)
    :base(options){  }

    
}