using AutoMapper;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLayer.Services.BaseCrud
{
    public class ItemTagCrudService : BaseCrudService<ItemTag, ItemTagModel>, IItemTagCrudService
    {
        protected override IRepository<ItemTag> BaseRepository
        {
            get
            {
                return cPUnitOfWork.ItemTagRepository;
            }
        }

        public ItemTagCrudService(ICPUnitOfWork cPUnitOfWork, IMapper mapper) : base(cPUnitOfWork, mapper)
        {
        }
    }
}