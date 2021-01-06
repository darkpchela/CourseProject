using AutoMapper;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLayer.Services.BaseCrud
{
    public class CommentsCrudService : BaseCrudService<Comment, CommentModel>, ICommentsCrudService
    {
        protected override IRepository<Comment> BaseRepository
        {
            get
            {
                return cPUnitOfWork.CommentsRepository;
            }
        }

        public CommentsCrudService(ICPUnitOfWork cPUnitOfWork, IMapper mapper) : base(cPUnitOfWork, mapper)
        {
        }
    }
}