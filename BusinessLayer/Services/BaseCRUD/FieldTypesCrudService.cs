using AutoMapper;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLayer.Services.BaseCrud
{
    public class FieldTypesCrudService : BaseCrudService<FieldType, FieldTypeModel>, IFieldTypesCrudService
    {
        protected override IRepository<FieldType> BaseRepository
        {
            get
            {
                return cPUnitOfWork.FieldTypesRepository;
            }
        }

        public FieldTypesCrudService(ICPUnitOfWork cPUnitOfWork, IMapper mapper) : base(cPUnitOfWork, mapper)
        {
        }
    }
}