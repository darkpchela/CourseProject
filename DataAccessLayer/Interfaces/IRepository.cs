using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);

        Task<TEntity> Get(int id);

        IQueryable<TEntity> GetAll();

        void Update(TEntity entity);

        Task Delete(int id);
    }
}