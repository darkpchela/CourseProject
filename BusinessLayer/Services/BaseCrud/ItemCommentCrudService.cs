using AutoMapper;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLayer.Services.BaseCrud
{
    public class ItemCommentCrudService : BaseCRUDService<ItemComment, ItemCommentModel>, IItemCommentCrudService
    {
        protected override IRepository<ItemComment> BaseRepository
        {
            get
            {
                return cPUnitOfWork.ItemCommentRepository;
            }
        }

        public ItemCommentCrudService(ICPUnitOfWork cPUnitOfWork, IMapper mapper) : base(cPUnitOfWork, mapper)
        {
        }
    }
}