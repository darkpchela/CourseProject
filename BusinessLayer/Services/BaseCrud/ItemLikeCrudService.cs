using AutoMapper;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLayer.Services.BaseCrud
{
    public class ItemLikeCrudService : BaseCrudService<ItemLike, ItemLikeModel>, IItemLikeCrudService
    {
        protected override IRepository<ItemLike> BaseRepository
        {
            get
            {
                return cPUnitOfWork.ItemLikeRepository;
            }
        }

        public ItemLikeCrudService(ICPUnitOfWork cPUnitOfWork, IMapper mapper) : base(cPUnitOfWork, mapper)
        {
        }
    }
}