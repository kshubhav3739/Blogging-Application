using System.Collections.Generic;

namespace BloggingApp.Models
{
    public interface IBlogRepository
    {
        Blog AddBlog(Blog blog);
        IEnumerable<Blog> AllBlogs();
        IEnumerable<Blog> AllBlogs(int userId, int currentBlog);
        Blog GetBlog(int id);
        Blog UpdateBlog(Blog blogChange);
        Blog DeleteBlog(int id);

    }
}