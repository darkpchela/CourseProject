using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Services.Repositories
{
    public class CommentsRepository : DbSetRepository<Comment>, ICommentsRepository
    {
        public CommentsRepository(CPDbContext dbContext) : base(dbContext)
        {
        }
    }
}