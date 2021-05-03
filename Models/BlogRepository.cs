using System.Collections.Generic;
using System.Linq;
namespace BloggingApp.Models
{
    public class BlogRepository : IBlogRepository
    {
        private readonly AppDbContext context;

        public BlogRepository(AppDbContext _context)
        {
            context = _context;
        }

        public Blog AddBlog(Blog blog)
        {
            context.Blogs.Add(blog);
            context.SaveChanges();
            return blog;
        }

        public IEnumerable<Blog> AllBlogs()
        {
            return context.Blogs;
        }

        public IEnumerable<Blog> AllBlogs(int userId, int currentBlog)
        {
            return context.Blogs.Where(s => s.userId == userId && s.id != currentBlog).Select(s => s);

        }

        public Blog DeleteBlog(int id)
        {
            Blog blog = context.Blogs.Find(id);
            if (blog != null)
            {
                context.Blogs.Remove(blog);
                context.SaveChanges();
            }
            return blog;
        }

        public Blog GetBlog(int id)
        {
            Blog blog = context.Blogs.Find(id);
            return blog;
        }

        public Blog UpdateBlog(Blog blogChange)
        {
            var blog = context.Blogs.Attach(blogChange);
            blog.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return blogChange;
        }
    }
}