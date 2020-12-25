using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Services.Repositories
{
    public class ItemCommentRepository : DbSetRepository<ItemComment>, IItemCommentRepository
    {
        public ItemCommentRepository(CPDbContext dbContext) : base(dbContext)
        {
        }
    }
}