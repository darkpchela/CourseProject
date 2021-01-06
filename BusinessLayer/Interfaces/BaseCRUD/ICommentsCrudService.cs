using BusinessLayer.Models;
using DataAccessLayer.Entities;

namespace BusinessLayer.Interfaces.BaseCrud
{
    public interface ICommentsCrudService : IBaseCRUDService<Comment, CommentModel>
    {
    }
}