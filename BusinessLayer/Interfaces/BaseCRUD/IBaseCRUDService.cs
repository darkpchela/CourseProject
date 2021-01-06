using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces.BaseCRUD
{
    public interface IBaseCRUDService<TEntity, TModel> where TEntity : class where TModel : class
    {
        Task CreateAsync(TModel model);

        Task<TModel> GetAsync(int id);

        Task<IEnumerable<TModel>> GetAll();

        Task Update(TModel model);

        Task Delete(int id);
    }
}