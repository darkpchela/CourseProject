using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Services.Repositories
{
    public class UsersRepository : DbSetRepository<User>, IUsersRepository
    {
        public UsersRepository(CPDbContext dbContext) : base(dbContext)
        {
        }
    }
}