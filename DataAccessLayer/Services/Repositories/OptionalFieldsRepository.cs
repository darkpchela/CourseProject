using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Services.Repositories
{
    public class OptionalFieldsRepository : DbSetRepository<OptionalField>, IOptionalFieldsRepository
    {
        public OptionalFieldsRepository(CPDbContext dbContext) : base(dbContext)
        {
        }
    }
}