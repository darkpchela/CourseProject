using AutoMapper;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLayer.Services.BaseCrud
{
    public class CollectionOptionalFieldCrudService : BaseCrudService<CollectionOptionalField, CollectionOptionalFieldModel>, ICollectionOptionalFieldCrudService
    {
        protected override IRepository<CollectionOptionalField> BaseRepository
        {
            get
            {
                return cPUnitOfWork.CollectionOptionalFieldRepository;
            }
        }

        public CollectionOptionalFieldCrudService(ICPUnitOfWork cPUnitOfWork, IMapper mapper) : base(cPUnitOfWork, mapper)
        {
        }
    }
}