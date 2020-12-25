using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Services.Repositories
{
    public class TagsRepository : DbSetRepository<Tag>, ITagsRepository
    {
        public TagsRepository(CPDbContext dbContext) : base(dbContext)
        {
        }
    }
}