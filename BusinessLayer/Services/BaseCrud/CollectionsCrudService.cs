using AutoMapper;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLayer.Services.BaseCrud
{
    public class CollectionsCrudService : BaseCrudService<Collection, CollectionModel>, ICollectionsCrudService
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