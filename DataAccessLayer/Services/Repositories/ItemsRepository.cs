using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Services.Repositories
{
    public class ItemsRepository : DbSetRepository<Item>, IItemsRepository
    {
        public ItemsRepository(CPDbContext dbContext) : base(dbContext)
        {
        }
    }
}