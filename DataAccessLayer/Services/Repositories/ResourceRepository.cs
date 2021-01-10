using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Services.Repositories
{
    public class ResourceRepository : DbSetRepository<Resource>, IResourceRepository
    {
        public ResourceRepository(CPDbContext cPDbContext) : base(cPDbContext)
        {
        }
    }
}