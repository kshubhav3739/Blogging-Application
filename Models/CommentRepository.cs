using System.Collections.Generic;
using System.Linq;
namespace BloggingApp.Models
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext context;

        public CommentRepository(AppDbContext _context)
        {
            context = _context;
        }

        public Comment AddComment(Comment comment)
        {
            context.Comments.Add(comment);
            context.SaveChanges();
            return comment;
        }

        public IEnumerable<Comment> AllComments(int blogId)
        {
            return context.Comments.Where(c => c.blogId == blogId).Select(c => c);
        }
    }
}