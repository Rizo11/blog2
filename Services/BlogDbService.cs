using blog2.Entities;
using Microsoft.EntityFrameworkCore;
using mvc6.Data;

namespace blog2.Services;

public class BlogDbService : IBlogDbService
{
    private readonly ILogger<BlogDbService> _logger;
    private readonly BlogDb _dataBase;

    public BlogDbService(ILogger<BlogDbService> logger, BlogDb blogDb)
    {
        _logger = logger;
        _dataBase = blogDb;
    }
    public Task<bool> ExistsBlogAsync(Guid id)
        => _dataBase.BlogsDb.AnyAsync(x => x.Id == id);

    public Task<List<Blog>> GetAllBlogsAsync()
        => _dataBase.BlogsDb.ToListAsync();

    public Task<Blog> GetBlogByIdAsync(Guid id)
        => _dataBase.BlogsDb.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<(bool IsSuccess, Exception Exception)> PostBlogAsync(Blog blog)
    {
        try
        {
            await _dataBase.BlogsDb.AddAsync(blog);
            await _dataBase.SaveChangesAsync();
            _logger.LogInformation($"New blog was added, ID = {blog.Id}");
            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"FAILED to add blog with id = {blog.Id} to the DB");
            return (false, e);
        }
    }

    public async Task<(bool IsSuccess, Exception Exception)> UpdateBlogAsync(Blog blog)
    {
        try
        {

            _dataBase.BlogsDb.Update(blog);
            await _dataBase.SaveChangesAsync();
            _logger.LogInformation($"Blog {blog.Id} was updated succesfully...");
            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"Error in updating blog, id={blog.Id}...", e.Message);

            return (false, e);
        }
    }

}