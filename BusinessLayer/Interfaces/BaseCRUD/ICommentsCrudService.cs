using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;

namespace BusinessLayer.Interfaces.BaseCrud
{
    public interface ICommentsCrudService : IBaseCrudService<Comment, CommentModel>
    {
    }
}