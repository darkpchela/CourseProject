using AutoMapper;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLayer.Services.BaseCrud
{
    public class OptionalFieldsCrudService : BaseCRUDService<OptionalField, OptionalFieldModel>, IOptionalFieldsCrudService
    {
        protected override IRepository<OptionalField> BaseRepository
        {
            get
            {
                return cPUnitOfWork.OptionalFieldsRepository;
            }
        }

        public OptionalFieldsCrudService(ICPUnitOfWork cPUnitOfWork, IMapper mapper) : base(cPUnitOfWork, mapper)
        {
        }
    }
}