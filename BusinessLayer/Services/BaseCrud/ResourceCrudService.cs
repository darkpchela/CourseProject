using AutoMapper;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLayer.Services.BaseCrud
{
    public class ResourceCrudService : BaseCrudService<Resource, ResourceModel>, IResourceCrudService
    {
        protected override IRepository<Resource> BaseRepository
        {
            get
            {
                return cPUnitOfWork.ResourceRepository;
            }
        }

        public ResourceCrudService(ICPUnitOfWork cPUnitOfWork, IMapper mapper) : base(cPUnitOfWork, mapper)
        {
        }
    }
}