using AutoMapper;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLayer.Services.BaseCrud
{
    public class TagsCrudService : BaseCrudService<Tag, TagModel>, ITagsCrudService
    {
        protected override IRepository<Tag> BaseRepository
        {
            get
            {
                return cPUnitOfWork.TagsRepository;
            }
        }

        public TagsCrudService(ICPUnitOfWork cPUnitOfWork, IMapper mapper) : base(cPUnitOfWork, mapper)
        {
        }
    }
}