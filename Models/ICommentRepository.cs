using System.Collections.Generic;

namespace BloggingApp.Models
{
    public interface ICommentRepository
    {
        Comment AddComment(Comment comment);
        IEnumerable<Comment> AllComments(int blogId);
    }
}
