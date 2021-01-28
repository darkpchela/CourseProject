using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Services.Repositories
{
    public abstract class DbSetRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbSet<TEntity> dbSet;

        protected readonly DbContext dbContext;

        public DbSetRepository(CPDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public virtual async Task Add(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public virtual async Task Delete(int id)
        {
            var entity = await dbSet.FindAsync(id);
            if (entity != null)
                dbSet.Remove(entity);
        }

        public virtual async Task<TEntity> Get(int id)
        {
            var entity = await dbSet.FindAsync(id);
            return entity;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return dbSet;
        }

        public virtual void Update(TEntity entity)
        {
            dbContext.ChangeTracker.Clear();
            dbSet.Update(entity);
        }
    }
}