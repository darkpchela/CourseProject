using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Services.Repositories
{
    public class ItemTagRepository : DbSetRepository<ItemTag>, IItemTagRepository
    {
        public ItemTagRepository(CPDbContext dbContext) : base(dbContext)
        {
        }
    }
}