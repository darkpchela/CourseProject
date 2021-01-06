using AutoMapper;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLayer.Services.BaseCrud
{
    public class CollectionsCrudService : BaseCRUDService<Collection, CollectionModel>, ICollectionsCrudService
    {
        protected override IRepository<Collection> BaseRepository
        {
            get
            {
                return cPUnitOfWork.CollectionsRepository;
            }
        }

        public CollectionsCrudService(ICPUnitOfWork cPUnitOfWork, IMapper mapper) : base(cPUnitOfWork, mapper)
        {
        }
    }
}