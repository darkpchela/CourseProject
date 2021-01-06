using AutoMapper;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLayer.Services.BaseCrud
{
    public class ThemesCrudService : BaseCRUDService<Theme, ThemeModel>, IThemesCrudService
    {
        protected override IRepository<Theme> BaseRepository
        {
            get
            {
                return cPUnitOfWork.ThemesRepository;
            }
        }

        public ThemesCrudService(ICPUnitOfWork cPUnitOfWork, IMapper mapper) : base(cPUnitOfWork, mapper)
        {
        }
    }
}