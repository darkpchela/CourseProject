using AutoMapper;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLayer.Services.BaseCrud
{
    public class ItemCommentCrudService : BaseCrudService<ItemComment, ItemCommentModel>, IItemCommentCrudService
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