using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Services.Repositories
{
    public class CollectionOptionalFieldRepository : DbSetRepository<CollectionOptionalField>, ICollectionOptionalFieldRepository
    {
        public CollectionOptionalFieldRepository(CPDbContext dbContext) : base(dbContext)
        {
        }
    }
}