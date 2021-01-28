using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Services.Repositories
{
    public class ItemsRepository : DbSetRepository<Item>, IItemsRepository
    {
        public ItemsRepository(CPDbContext dbContext) : base(dbContext)
        {
        }

        public override void Update(Item entity)
        {
            dbContext.ChangeTracker.Clear();
            dbSet.Update(entity);
            dbContext.Entry(entity).Property(nameof(Item.CreationDate)).IsModified = false;
        }
    }
}