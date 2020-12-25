using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Services.Repositories
{
    public class ThemesRepository : DbSetRepository<Theme>, IThemesRepository
    {
        public ThemesRepository(CPDbContext dbContext) : base(dbContext)
        {
        }
    }
}