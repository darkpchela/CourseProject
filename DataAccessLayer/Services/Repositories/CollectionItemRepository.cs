using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Services.Repositories
{
    public class CollectionItemRepository : DbSetRepository<CollectionItem>, ICollectionItemRepository
    {
        public CollectionItemRepository(CPDbContext dbContext) : base(dbContext)
        {
        }
    }
}