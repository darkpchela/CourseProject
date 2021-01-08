using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Services.Repositories
{
    public class FieldTypesRepository : DbSetRepository<FieldType>, IFieldTypesRepository
    {
        public FieldTypesRepository(CPDbContext cPDbContext) : base(cPDbContext)
        {
        }
    }
}