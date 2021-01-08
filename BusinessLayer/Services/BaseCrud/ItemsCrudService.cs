using AutoMapper;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLayer.Services.BaseCrud
{
    public class ItemsCrudService : BaseCrudService<Item, ItemModel>, IItemsCrudService
    {
        protected override IRepository<Item> BaseRepository
        {
            get
            {
                return cPUnitOfWork.ItemsRepository;
            }
        }

        public ItemsCrudService(ICPUnitOfWork cPUnitOfWork, IMapper mapper) : base(cPUnitOfWork, mapper)
        {
        }
    }
}