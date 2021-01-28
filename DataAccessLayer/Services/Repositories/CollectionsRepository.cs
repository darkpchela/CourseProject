using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Services.Repositories
{
    public class CollectionsRepository : DbSetRepository<Collection>, ICollectionsRepository
    {
        public CollectionsRepository(CPDbContext dbContext) : base(dbContext)
        {
        }

        public override void Update(Collection entity)
        {
            dbContext.ChangeTracker.Clear();
            dbSet.Update(entity);
            dbContext.Entry(entity).Property(nameof(Collection.CreationDate)).IsModified = false;
        }
    }
}