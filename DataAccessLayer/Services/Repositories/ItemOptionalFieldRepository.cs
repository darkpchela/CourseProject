using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Services.Repositories
{
    public class ItemOptionalFieldRepository : DbSetRepository<ItemOptionalField>, IItemOptionalFieldRepository
    {
        public ItemOptionalFieldRepository(CPDbContext dbContext) : base(dbContext)
        {
        }
    }
}