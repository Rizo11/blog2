namespace blog2.ViewModels;


public class UserViewModel
{
    public Guid Id { get; set; }
    public string Fullname { get; set; }  
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }   
    public int NOfPosts { get; set; }
    public int TotalLikes { get; set; }
    public int TotalDislikes { get; set; }
    public string Name { get; set; }
    
}