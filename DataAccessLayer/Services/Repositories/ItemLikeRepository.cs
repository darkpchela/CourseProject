using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Services.Repositories
{
    public class ItemLikeRepository : DbSetRepository<ItemLike>, IItemLikeRepository
    {
        public ItemLikeRepository(CPDbContext dbContext) : base(dbContext)
        {
        }
    }
}