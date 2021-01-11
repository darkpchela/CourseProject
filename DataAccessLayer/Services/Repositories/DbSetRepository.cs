using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Services.Repositories
{
    public abstract class DbSetRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> dbSet;

        private readonly DbContext dbContext;

        public DbSetRepository(CPDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await dbSet.FindAsync(id);
            if (entity != null)
                dbSet.Remove(entity);
        }

        public async Task<TEntity> Get(int id)
        {
            var entity = await dbSet.FindAsync(id);
            return entity;
        }

        public IQueryable<TEntity> GetAll()
        {
            return dbSet;
        }

        public void Update(TEntity entity)
        {
            dbContext.ChangeTracker.Clear();
            dbSet.Update(entity);
        }
    }
}