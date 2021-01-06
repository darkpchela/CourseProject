using AutoMapper;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLayer.Services.BaseCrud
{
    public class ItemOptionalFieldCrudService : BaseCRUDService<ItemOptionalField, ItemOptionalFieldModel>, IItemOptionalFieldCrudService
    {
        protected override IRepository<ItemOptionalField> BaseRepository
        {
            get
            {
                return cPUnitOfWork.ItemOptionalFieldRepository;
            }
        }

        public ItemOptionalFieldCrudService(ICPUnitOfWork cPUnitOfWork, IMapper mapper) : base(cPUnitOfWork, mapper)
        {
        }
    }
}