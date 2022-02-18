using blog2.Entities;
using Microsoft.EntityFrameworkCore;
using blog2.Data;

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
    public Task<bool> ExistsPostAsync(Guid id)
        => _dataBase.BlogsDb.AnyAsync(x => x.Id == id);

    public Task<List<Post>> GetAllPostsAsync()
        => _dataBase.BlogsDb.ToListAsync();

    public Task<Post> GetPostByIdAsync(Guid id)
        => _dataBase.BlogsDb.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<(bool IsSuccess, Exception Exception)> CreatePostAsync(Post post)
    {
        try
        {
            await _dataBase.BlogsDb.AddAsync(post);
            await _dataBase.SaveChangesAsync();
            _logger.LogInformation($"New post was added, ID = {post.Id}");
            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"FAILED to add post with id = {post.Id} to the DB");
            return (false, e);
        }
    }

    public async Task<(bool IsSuccess, Exception Exception)> UpdatePostAsync(Post post)
    {
        try
        {

            _dataBase.BlogsDb.Update(post);
            await _dataBase.SaveChangesAsync();
            _logger.LogInformation($"Post {post.Id} was updated succesfully...");
            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"Error in updating post, id={post.Id}...", e.Message);

            return (false, e);
        }
    }

    public async Task<(bool IsSuccess, Exception Exception)> DeletePostAsync(Guid Id)
    {
        try
        {
            var post = await _dataBase.BlogsDb.FirstOrDefaultAsync(x => x.Id == Id);
            post.Status = EPostStatus.Deleted;

            _dataBase.BlogsDb.Update(post);

            await _dataBase.SaveChangesAsync();
            _logger.LogInformation($"Post {post.Id} was deleted succesfully...");
            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"Error in deleting post, id={Id}...", e.Message);

            return (false, e);
        }
    }
    public List<Post> GetUserPosts(string userId)
    {
        return _dataBase.BlogsDb.Where(p => p.CreatedBy == Guid.Parse(userId)).ToList();
    }
}