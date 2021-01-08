using AutoMapper;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLayer.Services.BaseCrud
{
    public class CollectionItemCrudService : BaseCrudService<CollectionItem, CollectionItemModel>, ICollectionItemCrudService
    {
        protected override IRepository<CollectionItem> BaseRepository
        {
            get
            {
                return cPUnitOfWork.CollectionItemRepository;
            }
        }

        public CollectionItemCrudService(ICPUnitOfWork cPUnitOfWork, IMapper mapper) : base(cPUnitOfWork, mapper)
        {
        }
    }
}