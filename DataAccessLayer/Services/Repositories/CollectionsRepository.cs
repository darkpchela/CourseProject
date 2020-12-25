using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Services.Repositories
{
    public class CollectionsRepository : DbSetRepository<Collection>, ICollectionsRepository
    {
        public CollectionsRepository(CPDbContext dbContext) : base(dbContext)
        {
        }
    }
}